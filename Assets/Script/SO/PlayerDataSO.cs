using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData_", menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public Sprite avatar;
    public float moveSpeed;
    public float punchSpeed;
    public int health;
    public float enegy;
    public float basicDamage;
    public int numberPunchActiveSeries;
    public float recoveryTimeBlock;
    public AudioClip painSound;
    public AnimationClip comboSkill;
    public List<DamePunchSO> damePunchSO;
}