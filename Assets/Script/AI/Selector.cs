using System.Collections;
using System.Collections.Generic;
public class Selector : BTNode
{
    private List<BTNode> children;
    private int current = 0;

    public Selector(List<BTNode> nodes) { children = nodes; }

    public override NodeState Tick()
    {
        while (current < children.Count)
        {
            var state = children[current].Tick();
            if (state == NodeState.Running)
                return NodeState.Running;
            if (state == NodeState.Success)
            {
                current = 0;
                return NodeState.Success;
            }
            // state == Failure: thử con kế tiếp
            current++;
        }
        // tất cả con đều Failure
        current = 0;
        return NodeState.Failure;
    }
}

