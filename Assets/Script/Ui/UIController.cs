using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _skillSlider;

    protected override void Awake()
    {
        base.Awake();
    }
    void OnEnable()
    {
        PlayerStats.OnChangeNumberOfHealth += OnChangeNumberOfHealth;
    }
    private void OnChangeNumberOfHealth(float currHealth)
    {
        _healthSlider.value = currHealth;
    }
}
