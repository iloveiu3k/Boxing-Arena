using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BoxerData", menuName = "Data/BoxerData")]
public class BoxerDataSO : ScriptableObject
{
    public Sprite avatar;
    public float moveSpeed;
    public float punchSpeed;
    public int health;
    public float basicDamage;
    public int numberPunchActiveSeries;
    public float recoveryTimeBlock;
    public float attackRange;
    public float minSafedistance;
    public AudioClip painSound;
    public List<DamePunchSO> damePunchSO;
}