using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMovement : MonoBehaviour
{
    private GameManager _gameManager;
    private Bounds _boundsAreaMove;
    private bool _isMove = false;
    private bool _canMove = true ;
    private Rigidbody _rb;
    private Vector3 _positionRetreat;
    public Action<Vector3> OnMoveDirectionChanged;
    public Action OnStopMove;
    private bool _isApproach = false;
    private bool _isRetreat = false;
    private Boxer _boxer;
    private Vector3 diretMove;
    void Start()
    {
        _gameManager = GameManager.Instance;
        _boundsAreaMove = _gameManager.GetAreaBox();
        _rb = GetComponent<Rigidbody>();
        _boxer = GetComponent<Boxer>();
        _boxer.BoxingAI.OnRetreat += OnRetreat;
        _boxer.BoxingAI.OnApproach += OnApproach;
    }
    void FixedUpdate()
    {
        if(!_isMove || !_canMove)
        {
            return;
        }
        if(_isRetreat)
        {
            diretMove = (_positionRetreat - transform.position).normalized;
            if(Vector3.Distance(transform.position, _positionRetreat)<=0.1f)
            {
                _isMove = false;
                OnStopMove?.Invoke();
                _isRetreat = false;
                return;
            }
        }
        else if(_isApproach)
        {
            if(Vector3.Distance(transform.position, _boxer.BoxerFocus.GetBoxerFocus().transform.position)<=0.5f)
            {
                _isMove = false;
                OnStopMove?.Invoke();
                _isApproach = false;
                return;
            }
            diretMove = (_boxer.BoxerFocus.GetBoxerFocus().transform.position - transform.position).normalized;
        }
        _rb.velocity = diretMove * _boxer.BoxerDataSO.moveSpeed * Time.fixedDeltaTime ;
        OnMoveDirectionChanged?.Invoke(diretMove);
    }

    private Vector3 GetRetreatPointInCone(Transform opponent, float coneAngleDeg)
    {
        Vector3 baseDir = (transform.position - opponent.position).normalized;

        float half = coneAngleDeg * 0.5f;
        float angle = UnityEngine.Random.Range(-half, half);

        Vector3 randDir = Quaternion.AngleAxis(angle, Vector3.up) * baseDir;

        float dist = UnityEngine.Random.Range(1f, 1.5f);

        Vector3 desired = transform.position + randDir * dist;

        desired.x = Mathf.Clamp(desired.x, _boundsAreaMove.min.x, _boundsAreaMove.max.x);
        desired.z = Mathf.Clamp(desired.z, _boundsAreaMove.min.z, _boundsAreaMove.max.z);
        desired.y = transform.position.y;

        return desired;
    }
    private void OnRetreat()
    {
        _positionRetreat = GetRetreatPointInCone(_boxer.BoxerFocus.GetBoxerFocus().transform,140f);
        _isMove = true;
        _isApproach = false;
        _isRetreat = true;
    }
    private void OnApproach()
    {
        _positionRetreat = _boxer.BoxerFocus.GetBoxerFocus().transform.position;
        _isMove = true;
        _isApproach = true;
        _isRetreat = false;
    }
}
