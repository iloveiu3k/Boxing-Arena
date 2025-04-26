using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
[RequireComponent(typeof(BoxingAI))]
public class UtilityAIController : MonoBehaviour
{
    private GameObject _opponent;
    public float decisionInterval = 0.3f; 
    private float _nextDecisionTime;
    private BoxingAI _ai;
    private GameManager _gameManager;
    private Boxer _boxer;
    private float dist;
    private Dictionary<BehaviorType, float> _scores = new Dictionary<BehaviorType, float>();
    private List<BehaviorType> _keys = new List<BehaviorType>();
    private bool _canThink = true;

    void Awake()
    {
        _ai = GetComponent<BoxingAI>();
    }
    void Start()
    {
        _boxer = GetComponent<Boxer>();
        _gameManager = GameManager.Instance;
        _ai = _boxer.BoxingAI;
        PlayerAttack.OnCanActiveComboNow += OnCanActiveComboNow;
        PlayerEventAnimation.OnCompleteCombo += OnCompleteCombo;
        _boxer.BoxerStats.OnDying += OnDying;
    }
    void OnDisable()
    {
        PlayerAttack.OnCanActiveComboNow -= OnCanActiveComboNow;
        PlayerEventAnimation.OnCompleteCombo -= OnCompleteCombo;
    }
    void Update()
    {
        if (Time.time < _nextDecisionTime || !_canThink)
        {
            return;
        }
        if(_opponent == null)
        {
            FindOpponent();
            return;
        }
        dist = _ai.DistanceTo(_opponent.transform);
        _scores[BehaviorType.Block] = AIUtils.PlayerIsWindingUp(_opponent.transform, _boxer.BoxerAttack.GetIsBlock()) 
                                             ? 3f : 0f;
        _scores[BehaviorType.Retreat] = dist < _ai.minSafeDistance ? 2f : 0f;
        _scores[BehaviorType.Approach] = dist > _ai.attackRange ? 2f : 0f;
        _scores[BehaviorType.Attack] = dist <= _ai.attackRange ? 3f : 0f;
        _keys.Clear();
        _keys.AddRange(_scores.Keys);
        foreach (var k in _keys)
        {
            _scores[k] += UnityEngine.Random.Range(0f, 2f);
        }
        var best = _scores.Aggregate((a, b) => a.Value > b.Value ? a : b).Key;

        ExecuteBehavior(best);
        _nextDecisionTime = Time.time + decisionInterval;
    }

    private void ExecuteBehavior(BehaviorType beh)
    {
        switch (beh)
        {
            case BehaviorType.Block:
                _ai.Block();
                break;
            case BehaviorType.Retreat:
                _ai.Retreat();
                break;
            case BehaviorType.Approach:
                _ai.Approach();
                break;
            case BehaviorType.Attack:
                _ai.Attack();
                break;
            case BehaviorType.Idle:
                _ai.Idle();
                break;
        }
    }
    private void FindOpponent()
    {
        _boxer.BoxerFocus.FindNearestOponent();
        // Debug.Log(_boxer.BoxerFocus.GetBoxerFocus().name);
        _opponent = _boxer.BoxerFocus.GetBoxerFocus();
    }

    private void OnCanActiveComboNow()
    {
        if(Player.Instance.PlayerFocus.GetEnemyFocus() == gameObject)
        {
            _canThink = false;
            _boxer.BoxerAttack.SetIsBlock(false);
            ExecuteBehavior(BehaviorType.Idle);
        }
    }
    private void OnCompleteCombo()
    {
        if(!_canThink)
        {
            _canThink = true;
        }
    }
    private void OnDying()
    {
        _canThink = false;
    }
}