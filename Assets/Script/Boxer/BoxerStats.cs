using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoxerStats : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    private float _currHealth;
    public float CurrentHealth => _currHealth;
    private Boxer _boxer;
    private float _maxHealth;
    public Action OnTakeDamage; 
    public Action<float> OnChangeNumberOfHealth;
    void Start()
    {
        _boxer = GetComponent<Boxer>();
        _maxHealth = _boxer.BoxerDataSO.health;
        _currHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currHealth > 0)
        {
            return;
        }
    }
    public void TakeDamage(float amount)
    {
        if(_boxer.BoxerAttack.GetIsBlock())
        {
            return ;
        }
        _currHealth -= amount;
        OnTakeDamage?.Invoke();
        OnChangeNumberOfHealth?.Invoke((_currHealth*100f)/_maxHealth);
        GameObject ga = ObjectPooling.Instance.GetObjectFromPool(E_PoolName.HitHightlight,transform.position);
        ga.GetComponent<Billboard>().SetText(_currHealth);
    }
}
