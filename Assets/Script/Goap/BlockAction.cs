// File: Actions.cs
using UnityEngine;
using System.Collections.Generic;

// 1. Block
public class BlockAction : GoapAction {
    private GameObject _opp;
    private BoxingAI _ai; 
    public BlockAction(GameObject opp, BoxingAI ai) {
        _opp = opp;
        _ai = ai;
        AddPrecondition("playerIsWindingUp", true);
        AddEffect("hasBlocked", true);
        Cost = 1f;
    }
    public override bool CheckProceduralPrecondition(GameObject agent) => true;
    public override bool Perform(GameObject agent) {
        _ai.Block();
        IsDone = true;
        return true;
    }
}