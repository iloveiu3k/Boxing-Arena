using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : GoapAction {
    private BoxingAI _ai;
    public IdleAction(BoxingAI ai) {
        _ai = ai;
        AddEffect("isIdle", true);
        Cost = 0.5f;
    }
    public override bool CheckProceduralPrecondition(GameObject agent) => true;
    public override bool Perform(GameObject agent) {
        _ai.Idle();
        IsDone = true;
        return true;
    }
}
