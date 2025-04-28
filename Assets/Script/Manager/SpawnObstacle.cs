using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private List<ObstacleData> _obstacleDatas;
    [SerializeField] private List<Transform> _placesThrow;
    private List<GameObject> _zoneCircle;
    public float flightTime = 3f;
    private bool _canThrow = false;
    private Bounds _areaBox ;
    private float _timeSetup = 4;
    private int _maxTurnThrow ;
    private List<float> _percentRandomTurn;
    private void Start()
    {
        _placesThrow = GetComponentsInChildren<Transform>().ToList();
        _placesThrow.Remove(gameObject.transform);
        _areaBox = GameManager.Instance.GetAreaBox();
        _maxTurnThrow = GameManager.Instance.GetDifficultLevel();
        StartCoroutine(TimeWaitNetTurn(10 - _maxTurnThrow));
        _percentRandomTurn = CalculateListPercent(_maxTurnThrow);
    }
    private void NumberOfThrow()
    {
        int turn = RandomByPercent(_percentRandomTurn);
        _canThrow = false;
        for(int i = 0;i < turn; i++)
        {
            StartCoroutine(ThowObstacle());
        }
        float time = Mathf.Max((UIController.Instance.GetValueInterestingSlide()/10)- (_maxTurnThrow*0.5f),1);
        StartCoroutine(TimeWaitNetTurn(time));
    }
    void Update()
    {
        if(!_canThrow)
        {
            return;
        }
        NumberOfThrow();
    }
    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * 1f; 
        float randomX = Player.Instance.transform.position.x + randomDirection.x; 
        float randomZ = Player.Instance.transform.position.z + randomDirection.y; 

        float randomY = Player.Instance.transform.position.y; 

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        randomPosition.x = Mathf.Clamp(randomPosition.x, _areaBox.min.x, _areaBox.max.x);
        randomPosition.z = Mathf.Clamp(randomPosition.z, _areaBox.min.z, _areaBox.max.z);

        return randomPosition;
    }


    private IEnumerator ThowObstacle()
    {
        GameObject zone = ObjectPooling.Instance.GetObjectFromPool(E_PoolName.ZoneCircle,GetRandomPositionAroundPlayer());
        yield return new WaitForSeconds(_timeSetup);
        Throw(zone.transform.position);
        ObjectPooling.Instance.ReturnPool(zone, E_PoolName.ZoneCircle);
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
    private IEnumerator TimeWaitNetTurn(float second)
    {
        yield return new WaitForSeconds(second);
        _canThrow = true;
    }
    public int RandomByPercent(List<float> percents)
    {
        float r = Random.Range(0f, 100f);
        float cumulative = 0f;

        for (int i = 0; i < percents.Count; i++)
        {
            cumulative += percents[i];
            if (r < cumulative)
            {
                return i + 1;
            }
        }
        return 1; 
    }
    public List<float> CalculateListPercent(int number)
    {
        List<float> percents = new List<float>();
        int sum = 0;

        for (int i = 1; i <= number; i++)
        {
            sum += (number - i + 1);
        }

        for (int i = 1; i <= number; i++)
        {
            float r = (float)(number - i + 1) / sum * 100f;
            percents.Add(r);
        }
        return percents;
    }
}

