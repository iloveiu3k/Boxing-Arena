using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatAction : GoapAction {
    private GameObject _opp; 
    private BoxingAI _ai; 
    public RetreatAction(GameObject opp, BoxingAI ai) {
        _opp    = opp;
         _ai = ai;
        AddEffect("dist", _ai.minSafeDistance);
        Cost = 1f;
    }
    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        return _ai.DistanceTo(_opp.transform) < _ai.minSafeDistance;
    } 
    public override bool Perform(GameObject agent) {
        _ai.Retreat();
        IsDone = true;
        return true;
    }
}