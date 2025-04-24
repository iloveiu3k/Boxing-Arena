using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GoapPlanner 
{
    /// <summary>
    /// worldState và goalState là Dictionary&lt;string, object&gt;.
    /// availableActions: set các action khả dụng.
    /// </summary>
    public Queue<GoapAction> Plan(GameObject agent,
                                  HashSet<GoapAction> availableActions,
                                  Dictionary<string, object> worldState,
                                  Dictionary<string, object> goalState)
    {
        // Lọc action có precondition phù hợp
        var usableActions = new HashSet<GoapAction>();
        foreach (var a in availableActions) {
            a.ResetState();
            if (InState(a.Preconditions, worldState))
                usableActions.Add(a);
        }

        // Khởi tạo open/closed
        var start = new Node(null, 0, Heuristic(worldState, goalState), worldState, null);
        var open   = new List<Node> { start };
        var closed = new List<Node>();

        while (open.Count > 0) {
            open = open.OrderBy(n => n.F).ToList();
            var node = open[0];
            open.RemoveAt(0);
            closed.Add(node);

            if (InState(goalState, node.state)) {
                // build path
                return BuildPlan(node);
            }

            // mở rộng node
            foreach (var action in usableActions) {
                if (!InState(action.Preconditions, node.state)) continue;

                var newState = ApplyEffects(node.state, action.Effects);
                float cost   = node.g + action.GetCost(node.state);
                float h      = Heuristic(newState, goalState);
                var child    = new Node(node, cost, h, newState, action);

                if (closed.Any(n => InState(n.state, newState))) continue;
                var existing = open.FirstOrDefault(n => InState(n.state, newState));
                if (existing == null || cost < existing.g) {
                    open.Add(child);
                }
            }
        }

        // Không tìm được
        return null;
    }

    private static bool InState(IReadOnlyDictionary<string, object> req, Dictionary<string, object> state) {
        foreach (var kv in req) {
            if (!state.TryGetValue(kv.Key, out var val) || !val.Equals(kv.Value))
                return false;
        }
        return true;
    }

    private static Dictionary<string, object> ApplyEffects(Dictionary<string, object> oldState, IReadOnlyDictionary<string, object> effects) {
        var s = new Dictionary<string, object>(oldState);
        foreach (var e in effects) s[e.Key] = e.Value;
        return s;
    }

    private static float Heuristic(Dictionary<string, object> state, Dictionary<string, object> goal) {
        // đơn giản: đếm số goal chưa thỏa
        int count = 0;
        foreach (var kv in goal)
            if (!state.TryGetValue(kv.Key, out var v) || !v.Equals(kv.Value))
                count++;
        return count;
    }

    private static Queue<GoapAction> BuildPlan(Node node) {
        var stack = new Stack<GoapAction>();
        var n = node;
        while (n != null && n.action != null) {
            stack.Push(n.action);
            n = n.parent;
        }
        return new Queue<GoapAction>(stack);
    }
}
