using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxerAttack : MonoBehaviour
{
    private bool _isBlock = false;
    private Boxer _boxer;
    private bool _isComboPunch = false;
    private bool _isPunch = false;
    private int _countPunch = 0;
    private bool _canBlock = true; 

    public List<GameObject> _listOpponentEnter = new List<GameObject>();
    void Start()
    {
        _boxer = GetComponent<Boxer>();
        _boxer.BoxerEventAnimation.OnCompleteBlock += OnCompleteBlock;
        _boxer.BoxingAI.OnBlock += OnActiveBlock;
        _boxer.BoxingAI.OnAttack += OnAttack;
        _boxer.BoxerEventAnimation.OnRaiseDamage += OnRaiseDamage;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && _boxer.BoxerFocus.GetBoxerFocusList().Contains(other.transform.root.gameObject))
        {
            _listOpponentEnter.Add(other.transform.root.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && _boxer.BoxerFocus.GetBoxerFocusList().Contains(other.transform.root.gameObject))
        {
            _listOpponentEnter.Remove(other.transform.root.gameObject);
        }
    }
    private void OnAttack()
    {
        if(_isPunch || _isComboPunch)
        {
            return;
        }
        StartCoroutine(Punch());
    }
    private IEnumerator Punch()
    {
        if(_countPunch == _boxer.BoxerDataSO.numberPunchActiveSeries)
        {
            _boxer.BoxerAnimation.SeriesPunch();
            _countPunch = 0;
            _isComboPunch = true;
        }
        else
        {
            _boxer.BoxerAnimation.Punch();
            _countPunch++;
        }
        _isPunch = true;
        yield return new WaitForSeconds(_boxer.BoxerDataSO.punchSpeed);
        _isPunch = false ;
    }

    private void OnActiveBlock()
    {
        if(!_canBlock)
        {
            return;
        }
        _isBlock = true;
        _canBlock = false;
        _boxer.BoxerAnimation.Block();
    }

    private void OnCompleteCombo(bool combo)
    {
        _isComboPunch = combo;
    }

    private void OnCompleteBlock()
    {
        _isComboPunch = false;
        _isPunch = false;
        _isBlock = false;
        StartCoroutine(WaitRecoveryTimeBlock());
    }

    private void OnRaiseDamage()
    {
        var target = _boxer.BoxerFocus.GetBoxerFocus();
        if (_listOpponentEnter.Contains(target))
        {
            var dmg = target.GetComponent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(_boxer.BoxerDataSO.basicDamage);

            }
        }
    }

    private IEnumerator WaitRecoveryTimeBlock()
    {
        yield return new WaitForSeconds(_boxer.BoxerDataSO.recoveryTimeBlock);
        _isBlock = false;
        _canBlock = true;
    }

    public bool GetIsBlock()
    {
        return _isBlock;
    }
}
