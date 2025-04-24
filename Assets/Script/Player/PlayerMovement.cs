using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool _isMove = false;
    private bool _canMove = true;
    private Joystick _joystick;
    private float _speed;
    private Rigidbody _rb;
    private Camera _camera;
    private Vector3 _cameraForward, _cameraRight;
    public static Action<Vector3> OnMoveDirectionChanged;
    public static Action OnStanding;


    void OnEnable()
    {
        BotNavigationHandler.OnActiveBlock += OnStopMove;
        BotNavigationHandler.OnActiveCombo += OnStopMove;
        PlayerEventAnimation.OnCanMove += OnCanMove;
        Joystick.OnIsMove += OnIsMove;
    }

    void OnDisable()
    {
        BotNavigationHandler.OnActiveBlock -= OnStopMove;
        BotNavigationHandler.OnActiveCombo -= OnStopMove;
        PlayerEventAnimation.OnCanMove -= OnCanMove;
        Joystick.OnIsMove -= OnIsMove;

    }

    void Start()
    {
        _joystick = Joystick.Instance;
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _speed = Player.Instance.PlayerDataSO.moveSpeed;
        _cameraForward = _camera.transform.forward;
        _cameraForward.y = 0f;
        _cameraRight = _camera.transform.right;
        _cameraRight.y = 0f;
    }

    void FixedUpdate()
    {
        if (!_isMove || !_canMove)
        {
            return;
        }
        Vector3 movementDirection = (_cameraForward * _joystick.Direction.y) + (_cameraRight * _joystick.Direction.x);
        movementDirection.Normalize();
        Vector3 movement = movementDirection * _speed * Time.fixedDeltaTime;
        _rb.velocity = movement;
        OnMoveDirectionChanged?.Invoke(movementDirection);

    }

    private void OnStopMove()
    {
        _canMove = false;
        OnStanding?.Invoke();
    }

    private void OnIsMove(bool isMove)
    {
        _isMove = isMove;
        if (!isMove)
        {
            OnStanding?.Invoke();
        }
    }

    private void OnCanMove(bool canMove)
    {
        _canMove = canMove;
    }
}
