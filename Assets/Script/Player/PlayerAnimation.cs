using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private float _smoothXVelocity = 0f;
    private float _smoothYVelocity = 0f;

    void OnEnable()
    {
        PlayerMovement.OnMoveDirectionChanged += OnAdjustMovementDirection;
        PlayerMovement.OnStanding += OnStanding;
        PlayerEventAnimation.OnMoveSeriesPunch += OnMoveSeriesPunch;
        PlayerStats.OnTakeDamage += OnTakeDamage;
        PlayerEventAnimation.OnCompleteBlock += OnCompleteBlock;
        PlayerEventAnimation.OnCompleteSlip += OnCompleteSlip;
        PlayerEventAnimation.OnCompleteCombo += OnCompleteCombo;
        GameManager.OnWiner += OnWiner;
    }

    void OnDisable()
    {
        PlayerMovement.OnMoveDirectionChanged -= OnAdjustMovementDirection;
        PlayerMovement.OnStanding -= OnStanding;
        PlayerEventAnimation.OnMoveSeriesPunch -= OnMoveSeriesPunch;
        PlayerStats.OnTakeDamage -= OnTakeDamage;
        PlayerEventAnimation.OnCompleteBlock -= OnCompleteBlock;
        PlayerEventAnimation.OnCompleteSlip -= OnCompleteSlip;
        PlayerEventAnimation.OnCompleteCombo -= OnCompleteCombo;
        GameManager.OnWiner -= OnWiner;
    }

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnAdjustMovementDirection(Vector3 movementDirection)
    {
        float currentX = _animator.GetFloat("LeftRight");
        float currentY = _animator.GetFloat("FrontBack");

        float targetX = Vector3.Dot(transform.right, movementDirection);  
        float targetY = Vector3.Dot(transform.forward, movementDirection);

        float blendX = Mathf.SmoothDamp(currentX, targetX, ref _smoothXVelocity, 0.1f);
        float blendY = Mathf.SmoothDamp(currentY, targetY, ref _smoothYVelocity, 0.1f);

        _animator.SetFloat("LeftRight", blendX);
        _animator.SetFloat("FrontBack", blendY);
        _animator.SetBool("IsMove",true);

    }

    public void OnStanding()
    {
        _animator.SetFloat("LeftRight", 0f);
        _animator.SetFloat("FrontBack", 0f);
    }

    public void ComboKickKnee()
    {
        ResetTrigger();
        _animator.SetBool("IsCombo", true); 
        _animator.SetTrigger("ComboKnee");
    }

    public void Punch()
    {
        ResetTrigger();
        _animator.SetFloat("TypePunch",Random.Range(1,4));
        _animator.SetTrigger("Punch");
    }

    public void SeriesPunch()
    {
        ResetTrigger();
        _animator.SetBool("IsMove",false);
        _animator.SetTrigger("SeriesPunch");
    }

    public void Block()
    {
        ResetTrigger();
        _animator.SetTrigger("Block");
        _animator.SetBool("IsBlock",true);
    }

    private void ResetTrigger()
    {
        _animator.ResetTrigger("Block");
        _animator.ResetTrigger("SeriesPunch");
        _animator.ResetTrigger("Punch");
    }
    private void OnMoveSeriesPunch()
    {
        _animator.SetBool("IsMove",true);
    }
    private void OnTakeDamage(E_DamgeType type)
    {
        ResetTrigger();
        if(Player.Instance.PlayerStats.CurrentHealth<=0)
        {
            _animator.SetBool("IsKnock", true);
            _animator.SetTrigger("Knock");

        }
        if(type == E_DamgeType.Slip)
        {
            _animator.SetTrigger("Slip");
            _animator.SetBool("IsSlip", true);
        }else
        {
            _animator.SetTrigger("Beaten");
        }
    }
    private void OnCompleteBlock()
    {
        ResetTrigger();
        _animator.SetBool("IsBlock",false);
    }
    private void OnCompleteSlip()
    {
        ResetTrigger();
        _animator.SetBool("IsSlip", false);
    }
    private void OnCompleteCombo()
    {
        ResetTrigger();
        _animator.SetBool("IsCombo", false); 
    }
    private void OnWiner()
    {
        ResetTrigger();
        _animator.SetTrigger("Winner"); 
    }
}
