using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    private float _speedPunch;
    private int _numberOfPunchToActiveSeriesPunch;
    private float _basicDamage;
    private bool _isPunch = false;
    private bool _isComboPunch = false;
    private int _countPunch = 0;
    private Player _player;
    private bool _isBlock = false;
    private List<GameObject> _listEnemyEnter = new List<GameObject>();


    private void OnEnable()
    {
        BotNavigationHandler.OnActiveBlock += OnActiveBlock;
        BotNavigationHandler.OnActiveBlock += OnActiveBlock;
        PlayerEventAnimation.OnCompleteCombo += OnCompleteCombo;
        PlayerEventAnimation.OnCompleteBlock += OnCompleteBlock;
    }

    private void Start()
    {
        _player = Player.Instance;
        _speedPunch = _player.PlayerDataSO.punchSpeed;
        _numberOfPunchToActiveSeriesPunch = _player.PlayerDataSO.numberPunchActiveSeries;
        _basicDamage = _player.PlayerDataSO.basicDamage;
    }

    void Update()
    {
        if(_listEnemyEnter.Count > 0 && !_isPunch && !_isComboPunch)
        {
            StartCoroutine(Punch());
        }
        else if(_listEnemyEnter.Count <= 0)
        {
            _countPunch = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && other.transform.parent.CompareTag("Enemy"))
        {
            _listEnemyEnter.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && other.transform.parent.CompareTag("Enemy"))
        {
            _listEnemyEnter.Remove(other.gameObject);
        }
    }

    private IEnumerator Punch()
    {
        if(_countPunch ==_numberOfPunchToActiveSeriesPunch)
        {
            Player.Instance.PlayerAnimation.SeriesPunch();
            _countPunch = 0;
            _isComboPunch = true;
        }
        else
        {
            Player.Instance.PlayerAnimation.Punch();
            _countPunch++;
        }
        _isPunch = true;
        yield return new WaitForSeconds(_speedPunch);
        _isPunch = false ;
    }
    private void OnActiveBlock()
    {
        if(_isBlock)
        {
            return;
        }
        _isBlock = true;
        Player.Instance.PlayerAnimation.Block();
    }

    private void OnCompleteCombo(bool combo)
    {
        _isComboPunch = combo;
    }
    private void OnCompleteBlock()
    {
        _isComboPunch = false;
        _isPunch = false;
        StartCoroutine(WaitRecoveryTimeBlock());
    }

    private IEnumerator WaitRecoveryTimeBlock()
    {
        yield return new WaitForSeconds(Player.Instance.PlayerDataSO.recoveryTimeBlock);
        _isBlock = false;

    }
}
