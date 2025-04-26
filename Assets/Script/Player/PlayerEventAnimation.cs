using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerEventAnimation : MonoBehaviour
{
    public static Action<bool> OnCompleteSeries;
    public static Action<bool> OnCanMove;
    public static Action OnCompleteBlock;
    public static Action OnMoveSeriesPunch;
    public static Action OnRaiseDamage;
    public static Action OnCompleteSlip;
    public static Action OnCompleteCombo;

    
    private IEnumerator EventCompleteSeriesCombo()
    {
        yield return new WaitForSeconds(Player.Instance.PlayerDataSO.punchSpeed);
        OnCompleteSeries?.Invoke(false);
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
    private void CompleteSlip()
    {
        OnCompleteSlip?.Invoke();
    }
    private void CompleteCombo()
    {
        OnCompleteCombo?.Invoke();
    }
    private void Knock()
    {
        // GameObject parent = transform.parent.gameObject;
        // transform.parent = null;
        // parent.SetActive(false);
    }
}
