using System.Collections;
using System.Collections.Generic;
using System;
public class ActionNode : BTNode
{
    public Func<NodeState> Action;
    public ActionNode(Func<NodeState> act) { Action = act; }
    public override NodeState Tick() => Action();
}

