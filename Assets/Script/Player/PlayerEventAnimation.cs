using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerEventAnimation : MonoBehaviour
{
    public static Action<bool> OnCompleteCombo;
    public static Action<bool> OnCanMove;
    public static Action OnCompleteBlock;
    public static Action OnMoveSeriesPunch;


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
}
