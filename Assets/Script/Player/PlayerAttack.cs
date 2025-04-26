using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool _canBlock = true;
    private bool _isSlip = false;
    private List<GameObject> _listOpponentEnter = new List<GameObject>();
    public static Action OnRiseSkill;
    public static Action<bool> OnActiveBlockButton;
    public static Action OnCanActiveComboNow;
    private void OnEnable()
    {
        BotNavigationHandler.OnActiveBlock += OnActiveBlock;
        BotNavigationHandler.OnActiveCombo += OnActiveCombo;
        PlayerEventAnimation.OnCompleteSeries += OnCompleteSeries;
        PlayerEventAnimation.OnCompleteBlock += OnCompleteBlock;
        PlayerStats.OnTakeDamage += OnTakeDamage;
        PlayerEventAnimation.OnRaiseDamage += OnRaiseDamage;
        PlayerEventAnimation.OnCompleteSlip += OnCompleteSlip;
        PlayerEventAnimation.OnCompleteCombo += OnCompleteCombo;
    }
    private void OnDisable()
    {
        BotNavigationHandler.OnActiveBlock -= OnActiveBlock;
        BotNavigationHandler.OnActiveCombo -= OnActiveCombo;
        PlayerEventAnimation.OnCompleteSeries -= OnCompleteSeries;
        PlayerEventAnimation.OnCompleteBlock -= OnCompleteBlock;
        PlayerStats.OnTakeDamage -= OnTakeDamage;
        PlayerEventAnimation.OnRaiseDamage -= OnRaiseDamage;
        PlayerEventAnimation.OnCompleteSlip -= OnCompleteSlip;
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
        if(_listOpponentEnter.Count > 0 && !_isPunch && !_isComboPunch)
        {
            StartCoroutine(Punch());
        }
        else if(_listOpponentEnter.Count <= 0)
        {
            _countPunch = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && other.transform.parent.CompareTag("Enemy"))
        {
            _listOpponentEnter.Add(other.transform.parent.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && other.transform.parent.CompareTag("Enemy"))
        {
            _listOpponentEnter.Remove(other.transform.parent.gameObject);
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
        if(!_canBlock || _isSlip)
        {
            return;
        }
        _isBlock = true;
        _canBlock = false;
        Player.Instance.PlayerAnimation.Block();
        BotNavigationHandler.Instance.OnActiveBlockButton(false);
        _player.PlayerMovement.OnStopMove();
    }

    private void OnCompleteSeries(bool combo)
    {
        _isComboPunch = combo;
    }
    private void OnCompleteBlock()
    {
        _isComboPunch = false;
        _isPunch = false;
        _isBlock =false;
        StartCoroutine(WaitRecoveryTimeBlock());
    }

    private void OnTakeDamage(E_DamgeType type)
    {
        if(type == E_DamgeType.Slip)
        {
            _isSlip = true;
        }
        _isComboPunch = false;
    }
    private void OnRaiseDamage()
    {
        var target = _player.PlayerFocus.GetEnemyFocus();
        if (_listOpponentEnter.Contains(target))
        {
            var dmg = target.GetComponent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(_player.PlayerDataSO.basicDamage,E_DamgeType.Punch);
                OnRiseSkill?.Invoke();
            }
        }
    }
    private void OnActiveCombo()
    {
        if(_isBlock || _isSlip)
        {
            return;
        }
        if(_listOpponentEnter.Contains(_player.PlayerFocus.GetEnemyFocus()))
        {
            OnCanActiveComboNow?.Invoke();
            _player.PlayerAnimation.ComboKickKnee();
        }
    }
    private void OnCompleteCombo()
    {
        _isComboPunch = false;
        _isPunch = false;
    }
    private void OnCompleteSlip()
    {
        _isSlip = false;
    }
    private IEnumerator WaitRecoveryTimeBlock()
    {
        yield return new WaitForSeconds(Player.Instance.PlayerDataSO.recoveryTimeBlock);
        _canBlock = true;
        OnActiveBlockButton?.Invoke(true);
    }
    public bool GetIsBlock()
    {
        return _isBlock;
    }
}
