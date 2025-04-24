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
    private List<GameObject> _listOpponentEnter = new List<GameObject>();
    void Start()
    {
        _boxer = GetComponent<Boxer>();
        _boxer.BoxerEventAnimation.OnCompleteBlock += OnCompleteBlock;
        _boxer.BoxingAI.OnBlock += OnActiveBlock;
        _boxer.BoxingAI.OnAttack += OnAttack;
    }
        void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && _boxer.BoxerFocus.GetBoxerFocusList().Contains(other.gameObject))
        {
            _listOpponentEnter.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ColliderDamage") && _boxer.BoxerFocus.GetBoxerFocusList().Contains(other.gameObject))
        {
            _listOpponentEnter.Remove(other.gameObject);
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
        if(_isBlock)
        {
            return;
        }
        _isBlock = true;
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
        StartCoroutine(WaitRecoveryTimeBlock());
    }

    private IEnumerator WaitRecoveryTimeBlock()
    {
        yield return new WaitForSeconds(_boxer.BoxerDataSO.recoveryTimeBlock);
        _isBlock = false;

    }

    public bool GetIsBlock()
    {
        return _isBlock;
    }
}
