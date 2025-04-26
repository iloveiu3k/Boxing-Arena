using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeelBanana : Obstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            isFloor = true;
        }
        else if(other.CompareTag("ColliderDamage") && isFloor )
        {
            if(!other.transform.parent.CompareTag("Player"))
            {
                return;
            }
            other.transform.parent.GetComponent<PlayerStats>().TakeDamage(obstacleData.dame,E_DamgeType.Slip);
            ObjectPooling.Instance.ReturnPool(gameObject,obstacleData.poolName);
        }
    }
}
