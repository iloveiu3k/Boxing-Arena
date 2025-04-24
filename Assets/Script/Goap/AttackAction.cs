using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : GoapAction {
    private GameObject _opp; 
    private BoxingAI _ai; 
    private Boxer boxer;
    public AttackAction(GameObject opp, BoxingAI ai) {
        _opp    = opp; 
        _ai = ai;
        AddEffect("hasAttacked", true);
        Cost = 1f;
    }
    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        return _ai.DistanceTo(_opp.transform) <= _ai.attackRange;
    } 
    public override bool Perform(GameObject agent) {
        _ai.Attack();
        IsDone = true;
        return true;
    }
}
