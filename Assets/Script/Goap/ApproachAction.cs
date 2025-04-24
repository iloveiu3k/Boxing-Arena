using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ApproachAction : GoapAction {
    private GameObject _opp;
    private BoxingAI _ai; 
    private Boxer boxer;
    public ApproachAction(GameObject opp, BoxingAI ai) {
        _opp    = opp;
        _ai = ai;
        AddEffect("dist", ai.attackRange);
        Cost = 1f;
    }
    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        return _ai.DistanceTo(_opp.transform) > _ai.attackRange;
    }
    public override bool Perform(GameObject agent) {
        _ai.Approach();
        IsDone = true;
        return true;
    }
}
