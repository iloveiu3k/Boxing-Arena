using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerStats : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    private float _currHealth;
    private float _maxHealth;
    public float CurrentHealth => _currHealth;
    public static Action OnTakeDamage; 
    public static Action<float> OnChangeNumberOfHealth;

    void Start()
    {
        _maxHealth = Player.Instance.PlayerDataSO.health;
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
        if(Player.Instance.PlayerAttack.GetIsBlock())
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
