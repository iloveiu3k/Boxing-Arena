using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public TMP_Text _tmp;
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Giúp chữ không bị ngược
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
    private void RandomNearbyPosition( )
    {
        float range = 0.2f;
        float randomX = Random.Range(-range, range);
        float randomY = Random.Range(0.5f, 0.6f);
        float randomZ = Random.Range(-range, range);
        transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
    }
    void OnEnable()
    {
        RandomNearbyPosition();
    }
    void OnDisable()
    {
        ObjectPooling.Instance.ReturnPool(gameObject,E_PoolName.HitHightlight);
    }

    public void SetText(float value)
    {
        int intValue = Mathf.FloorToInt(value);
        intValue = Mathf.Max(intValue, 0);
        _tmp.text = intValue.ToString();
    }
}