using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{
    private BoxerMovement _boxerMovement;
    public BoxerMovement BoxerMovement { get => _boxerMovement; }

    private BoxerAnimation _boxerAnimation;
    public BoxerAnimation BoxerAnimation { get => _boxerAnimation; }

    private BoxerAttack _boxerAttack;
    public BoxerAttack BoxerAttack { get => _boxerAttack; }

    private BoxerFocus _boxerFocus;
    public BoxerFocus BoxerFocus { get => _boxerFocus; }

    private BoxingAI _boxingAI;
    public BoxingAI BoxingAI { get => _boxingAI; }

    private BoxerEventAnimation _boxerEventAnimation;
    public BoxerEventAnimation BoxerEventAnimation  { get => _boxerEventAnimation; }
    private BoxerStats _boxerStats;
    public BoxerStats BoxerStats  { get => _boxerStats; }
    [SerializeField] private BoxerDataSO _boxerDataSO;
    public BoxerDataSO BoxerDataSO { get => _boxerDataSO; }

    void Awake()
    {
        _boxerMovement = GetComponent<BoxerMovement>();
        _boxerAnimation = GetComponent<BoxerAnimation>();
        _boxerAttack = GetComponent<BoxerAttack>();
        _boxerFocus = GetComponent<BoxerFocus>(); 
        _boxingAI = GetComponent<BoxingAI>();
        _boxerEventAnimation = GetComponentInChildren<BoxerEventAnimation>();
        _boxerStats = GetComponent<BoxerStats>();
    }
}
