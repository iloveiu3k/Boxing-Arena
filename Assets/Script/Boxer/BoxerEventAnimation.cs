using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoxerEventAnimation : MonoBehaviour
{
    public Action<bool> OnCompleteCombo;
    public Action<bool> OnCanMove;
    public Action OnCompleteBlock;
    public Action OnMoveSeriesPunch;
    public Action OnRaiseDamage;

    private IEnumerator EventCompleteSeriesCombo()
    {
        yield return new WaitForSeconds(Player.Instance.PlayerDataSO.punchSpeed);
        OnCompleteCombo?.Invoke(false);
    }

    private void SetCanMove()
    {
        OnCanMove?.Invoke(true);
    }

    private void CompleteBlock()
    {
        OnCompleteBlock?.Invoke();
    }
    private void SetIsMoveSeriesPunch()
    {
        OnMoveSeriesPunch?.Invoke();
    }
    private void RaiseDamage()
    {
        OnRaiseDamage?.Invoke();
    }
    private void Knock()
    {
        // GameObject parent = transform.parent.gameObject;
        // transform.parent = null;
        // parent.SetActive(false);
    }
}
