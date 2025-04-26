using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoxerStats : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    private float _currHealth;
    public float CurrentHealth => _currHealth;
    public bool Dying { get; set; } = false;

    private Boxer _boxer;
    private float _maxHealth;
    public Action<E_DamgeType> OnTakeDamage; 
    public Action<float> OnChangeNumberOfHealth;
    public Action OnDying;
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
    public void TakeDamage(float amount,E_DamgeType type)
    {
        if(_boxer.BoxerAttack.GetIsBlock() || _currHealth <= 0)
        {
            return ;
        }
        _currHealth -= amount;
        if(_currHealth <= 0)
        {
            OnDying?.Invoke();
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.freezeRotation = true;
            Dying = true;
            if(gameObject.CompareTag("Enemy"))
            {
                GameManager.Instance.FallNumberEnemyDying();
            }
            StartCoroutine(ActiveDying());
            
        }
        OnTakeDamage?.Invoke(type);
        OnChangeNumberOfHealth?.Invoke((_currHealth*100f)/_maxHealth);
        GameObject ga = ObjectPooling.Instance.GetObjectFromPool(E_PoolName.HitHightlight,transform.position);
        ga.GetComponent<Billboard>().SetText(_currHealth);
    }
    private IEnumerator ActiveDying()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
