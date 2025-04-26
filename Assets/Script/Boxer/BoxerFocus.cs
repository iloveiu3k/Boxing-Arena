using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BoxerFocus : MonoBehaviour
{
    public GameObject _boxerFocus;
    public float ScopeFocus = 10f;
    private bool _stopFocus = false;
    private Boxer _boxer; 
    private List<GameObject> _boxerFocusList = new List<GameObject>();
    private GameObject _lastFocus;
    private bool _isSwitching;
    public Action<GameObject> OnChangeFocus;


    void Start()
    {
        _boxer = GetComponent<Boxer>();
        if(gameObject.CompareTag("Enemy"))
        {
            _boxerFocusList = GameObject.FindGameObjectsWithTag("League").ToList();
            _boxerFocusList.Add(Player.Instance.gameObject);
        }
        else
        {
            _boxerFocusList = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }
        InvokeRepeating("FindNearestOponent", 0f, 0.5f);
        _boxer.BoxerStats.OnDying += OnStopFocus;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ScopeFocus);
    }

    void LateUpdate()
    {
        if (_boxerFocus == null || _isSwitching || _stopFocus)
        {
            return;
        }
        transform.LookAt(_boxerFocus.transform);
    }

    public void FindNearestOponent()
    {
        var candidate = _boxerFocusList
            .Where(e => e != null && e.GetComponent<IDamageable>().Dying != true)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude )
            .FirstOrDefault();

        if (candidate != null && candidate != _boxerFocus)
        {
            _lastFocus = _boxerFocus;
            _boxerFocus = candidate;
            OnChangeFocus?.Invoke(_boxerFocus);
            if (_isSwitching) StopCoroutine(nameof(RotateToNewFocus));
            StartCoroutine(nameof(RotateToNewFocus));
        }
    }

    IEnumerator RotateToNewFocus()
    {
        _isSwitching = true;
        Quaternion from = transform.rotation;
        Vector3 dir = (_boxerFocus.transform.position - transform.position).normalized;
        Quaternion to = Quaternion.LookRotation(dir);

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

    public GameObject GetBoxerFocus()
    {
        return _boxerFocus;
    }

    public void SetBoxerFocusList(List<GameObject> list)
    {
        _boxerFocusList = list;
    }
    public List<GameObject> GetBoxerFocusList()
    {
        return _boxerFocusList;
    }
    void OnDisable()
    {
        StopCoroutine(nameof(RotateToNewFocus));
    }
    private void OnStopFocus()
    {
        _stopFocus = true;
        CancelInvoke("FindNearestOponent");

    }
}
