using System.Collections;
using System.Collections.Generic;
using System;
public class ConditionNode : BTNode
{
    public Func<bool> Condition;
    public ConditionNode(Func<bool> cond) { Condition = cond; }
    public override NodeState Tick()
        => Condition() ? NodeState.Success : NodeState.Failure;
}
