using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleData obstacleData;
    protected bool isFloor = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isFloor)
        {
            Player.Instance.PlayerStats.TakeDamage(obstacleData.dame,E_DamgeType.Punch);
            StartCoroutine(ReturnPool());
            isFloor = true;

        }  
        else if(other.CompareTag("Floor"))
        {
            StartCoroutine(ReturnPool());
            isFloor = true;
        }
    }
    private IEnumerator ReturnPool()
    {
        yield return new WaitForSeconds(3f);
        ObjectPooling.Instance.ReturnPool(gameObject,obstacleData.poolName);
    }
}
