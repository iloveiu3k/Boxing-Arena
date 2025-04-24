using System.Collections.Generic;
public class Sequence : BTNode
{
    private List<BTNode> children;
    private int current = 0;

    public Sequence(List<BTNode> nodes) { children = nodes; }

    public override NodeState Tick()
    {
        while (current < children.Count)
        {
            var state = children[current].Tick();
            if (state == NodeState.Running)
                return NodeState.Running;
            if (state == NodeState.Failure)
            {
                current = 0;
                return NodeState.Failure;
            }
            // state == Success: tiến sang node tiếp theo
            current++;
        }
        // tất cả con đều Success
        current = 0;
        return NodeState.Success;
    }
}
