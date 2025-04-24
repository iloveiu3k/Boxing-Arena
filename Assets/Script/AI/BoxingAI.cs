using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxingAI : MonoBehaviour
{
    public float attackRange, minSafeDistance, optimalDistance;
    private Boxer _boxer;
    public Action OnBlock;
    public Action OnRetreat;
    public Action OnAttack;
    public Action OnApproach;
    public Action OnIdle;
    private GameManager _gameManager;
    void Start()
    {
        _boxer = GetComponent<Boxer>();
        _gameManager = GameManager.Instance;
    }
    public void Block()    
    {
        OnBlock?.Invoke();
    }
    public void Attack()   
    {
        OnAttack?.Invoke();
    }
    public void Retreat()  
    { 
        OnRetreat?.Invoke();
    }
    public void Approach() 
    {
        OnApproach?.Invoke();
    }
    public void Idle()     
    {
        OnIdle?.Invoke();
    }

    public float DistanceTo(Transform t) => Vector3.Distance(transform.position, t.position);

    public bool CanActionNode()
    {
        float randomValue = UnityEngine.Random.Range(0f, 10f);
        Debug.Log(randomValue);
        return randomValue <= _gameManager.GetDifficultLevel();    
    }

}

