using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private List<ObstacleData> _obstacleDatas;
    [SerializeField] private List<Transform> _placesThrow;

    public float flightTime = 3f;
    private void Start()
    {
        _placesThrow = GetComponentsInChildren<Transform>().ToList();
        _placesThrow.Remove(gameObject.transform);
        Throw(new Vector3(0, 0, 0));
    }

    public void Throw(Vector3 postion)
    {
        int index = Random.Range(0, _obstacleDatas.Count);
        ObstacleData randomObstacle = _obstacleDatas[index];
        index = Random.Range(0, _placesThrow.Count);
        Vector3 place = _placesThrow[index].position;
        GameObject obj = ObjectPooling.Instance.GetObjectFromPool(randomObstacle.poolName,place);
        Vector3 velocity = CalculateLaunchVelocity(
            place,
            postion,
            flightTime
        );

        obj.GetComponent<Rigidbody>().velocity = velocity;
    }

    Vector3 CalculateLaunchVelocity(Vector3 origin, Vector3 target, float time)
    {
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);

        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        float vy = y / time + 0.5f * Physics.gravity.magnitude * time;
        float vxz = xz / time;

        Vector3 result = toTargetXZ.normalized * vxz;
        result.y = vy;
        return result;
    }
}

