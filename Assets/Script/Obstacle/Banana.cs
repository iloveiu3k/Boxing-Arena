using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Obstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Floor"))
        {
            ObjectPooling.Instance.GetObjectFromPool(E_PoolName.PeelBanana, transform.position);
            ObjectPooling.Instance.ReturnPool(gameObject,obstacleData.poolName);
        }
    }
}
