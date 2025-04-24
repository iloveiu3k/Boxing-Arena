using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float attackRange;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Vẽ wire sphere quanh transform.position với bán kính = range
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
