using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BotNavigationHandler : Singleton<BotNavigationHandler>
{
    public static Action OnActiveBlock;
    public static Action OnActiveCombo;
    [SerializeField] private Button _blockButton;
    [SerializeField] private Button _comboButton;

    protected override void Awake()
    {
        base.Awake();
        OnActiveSkillButton(false);
    }

    void OnEnable()
    {
        _blockButton.onClick.AddListener(OnCallPlayerToBlock);
        _comboButton.onClick.AddListener(OnCallPlayerToCombo);
        PlayerAttack.OnActiveBlockButton += OnActiveBlockButton;
        PlayerAttack.OnCanActiveComboNow += OnCanActiveComboNow;
        PlayerEventAnimation.OnCompleteCombo += OnCompleteCombo;
    }
    void OnDisable()
    {
        PlayerAttack.OnActiveBlockButton -= OnActiveBlockButton;
        PlayerAttack.OnCanActiveComboNow -= OnCanActiveComboNow;
        PlayerEventAnimation.OnCompleteCombo -= OnCompleteCombo;
    }
    private void OnCallPlayerToBlock()
    {
        OnActiveBlock?.Invoke();
    }
    private void OnCallPlayerToCombo()
    {
        OnActiveCombo?.Invoke();
        
    }
    public void OnActiveBlockButton(bool active)
    {
        _blockButton.interactable = active;
    }
    public void OnActiveSkillButton(bool active)
    {
        _comboButton.interactable = active;
    }
    private void OnCanActiveComboNow()
    {
        OnActiveSkillButton(false);
    }
    private void OnCompleteCombo()
    {
        OnActiveSkillButton(false);
    }
}
