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
    public bool Dying { get; set;} = false;
    public static Action<E_DamgeType> OnTakeDamage; 
    public static Action<float> OnChangeNumberOfHealth;
    public static Action OnDying;
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
    public void TakeDamage(float amount,E_DamgeType type)
    {
        if(Player.Instance.PlayerAttack.GetIsBlock() || _currHealth < 0)
        {
            return ;
        }
        _currHealth -= amount;
        if(_currHealth<=0)
        {
            OnDying?.Invoke();
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.freezeRotation = true;
            Dying = true;
        }
        OnTakeDamage?.Invoke(type);
        OnChangeNumberOfHealth?.Invoke((_currHealth*100f)/_maxHealth);
        GameObject ga = ObjectPooling.Instance.GetObjectFromPool(E_PoolName.HitHightlight,transform.position);
        ga.GetComponent<Billboard>().SetText(_currHealth);
    }
}
