using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BotNavigationHandler : MonoBehaviour
{
    public static Action OnActiveBlock;
    public static Action OnActiveCombo;
    [SerializeField] private Button _blockButton;
    [SerializeField] private Button _comboButton;

    void OnEnable()
    {
        _blockButton.onClick.AddListener(OnCallPlayerToBlock);
        _comboButton.onClick.AddListener(OnCallPlayerToCombo);

    }

    private void OnCallPlayerToBlock()
    {
        OnActiveBlock?.Invoke();
    }
    private void OnCallPlayerToCombo()
    {
        OnActiveCombo?.Invoke();
    }
}
