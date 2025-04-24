using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerFocus : MonoBehaviour
{
    private GameObject _enemyFocus;
    public float ScopeFocus = 10f;
    private List<GameObject> _enemyFocusList;
    private GameObject _lastFocus;       
    private bool _isSwitching;      

    void Start()
    {
        _enemyFocusList = UnitsManager.Instance.EnemyList;
        InvokeRepeating("FindNearestEnemy", 0f, 0.5f);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ScopeFocus);
    }

    void Update()
    {
        if(_enemyFocusList.Count <=0 )
        {
            return;
        }
        if(!_enemyFocus)
        {
            FindNearestEnemy();
        }
    }

    void LateUpdate()
    {
        if (_enemyFocus == null || _isSwitching) 
        {
            return;
        }
        transform.LookAt(_enemyFocus.transform);
    }

    private void FindNearestEnemy()
    {
        var candidate = _enemyFocusList
            .Where(e => e != null)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (candidate != null && candidate != _enemyFocus)
        {
            _lastFocus    = _enemyFocus;
            _enemyFocus   = candidate;
            if (_isSwitching) StopCoroutine(nameof(RotateToNewFocus));
            StartCoroutine(nameof(RotateToNewFocus));
        }
    }

    IEnumerator RotateToNewFocus()
    {
        _isSwitching = true;

        Quaternion from = transform.rotation;
        Vector3   dir   = (_enemyFocus.transform.position - transform.position).normalized;
        Quaternion to   = Quaternion.LookRotation(dir);

        float t = 0f;

        while (t < 1f)
        {
            t += 0.01f;
            transform.rotation = Quaternion.Slerp(from, to, t);
            yield return null;
        }

        // chắc chắn dính đích
        transform.rotation = to;
        _isSwitching = false;
    }
    public GameObject GetEnemyFocus()
    {
        return _enemyFocus;
    }
}
