using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _interestSliderValue;
    [SerializeField] private Slider _skillSlider;
    [SerializeField] private GameObject _preventPanel;
    [SerializeField] private Button _exitButton;
    public float decreaseAmount = 2f;
    public float updateInterval = 1f;
    protected override void Awake()
    {
        base.Awake();

    }
    void OnEnable()
    {
        PlayerStats.OnChangeNumberOfHealth += OnChangeNumberOfHealth;
        PlayerAttack.OnRiseSkill += OnRiseSkill;
        PlayerAttack.OnCanActiveComboNow += OnCanActiveComboNow;
        PlayerEventAnimation.OnCompleteCombo += OnCompleteCombo;
        _exitButton.onClick.AddListener(OnExitToMenu);
        PlayerStats.OnDying += OnEnablePreventPanel;
        GameManager.OnWiner += OnEnablePreventPanel;
        // PlayerStats.OnLoser += OnEnablePreventPanel;
        _skillSlider.value = 90;
        _healthSlider.value = 100;
        _interestSliderValue.value = 100;
    }
    void OnDisable()
    {
        PlayerStats.OnChangeNumberOfHealth -= OnChangeNumberOfHealth;
        PlayerAttack.OnRiseSkill -= OnRiseSkill;
        PlayerAttack.OnCanActiveComboNow -= OnCanActiveComboNow;
        PlayerEventAnimation.OnCompleteCombo -= OnCompleteCombo;
        PlayerStats.OnDying -= OnEnablePreventPanel;
        GameManager.OnWiner -= OnEnablePreventPanel;
    }
    private void Start()
    {
        InvokeRepeating("DecreaseSliderValue", 2f, updateInterval);
    }
    private void OnChangeNumberOfHealth(float currHealth)
    {
        _healthSlider.value = currHealth;
    }
    private void OnRiseSkill()
    {
        _skillSlider.value += 5;
        _interestSliderValue.value += 5; 
        if(_skillSlider.value>=100)
        {
            BotNavigationHandler.Instance.OnActiveSkillButton(true);
        }
    }
    public void SetActivePreventPanel(bool active)
    {
        _preventPanel.SetActive(active);
    }
    private void OnCanActiveComboNow()
    {
        SetActivePreventPanel(true);
        _skillSlider.value = 0;
    }
    private void OnCompleteCombo()
    {
        SetActivePreventPanel(false);
    }
    public float GetValueInterestingSlide()
    {
        return _interestSliderValue.value;
    }
    private void DecreaseSliderValue()
    {
        if (_interestSliderValue.value > 0)
        {
            _interestSliderValue.value -= decreaseAmount; 
        }
        else
        {
            _interestSliderValue.value = 0; 
        }
    }
    private void OnEnablePreventPanel()
    {
        SetActivePreventPanel(true);
    }
    private void OnExitToMenu()
    {
        DataScene.Instance.AsyncLoader.LoadLevel("Menu");
    }
}
