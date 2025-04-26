using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PunchData_", menuName = "Data/PunchData_")]
public class DamePunchSO : ScriptableObject
{
    public E_PunchType punchType;
    public float punchDamage;
    public AudioClip punchSound;
    public AnimationClip punchAnim;
}