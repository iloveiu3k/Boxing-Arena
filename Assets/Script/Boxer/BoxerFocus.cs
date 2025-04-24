using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BoxerFocus : MonoBehaviour
{
    private GameObject _boxerFocus;
    public float ScopeFocus = 10f;

    private List<GameObject> _boxerFocusList;
    private GameObject _lastFocus;
    private bool _isSwitching;
    public Action<GameObject> OnChangeFocus;

    void Start()
    {
        InvokeRepeating("FindNearestOponent", 0f, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ScopeFocus);
    }

    void LateUpdate()
    {
        if (_boxerFocus == null || _isSwitching)
        {
            return;
        }
        transform.LookAt(_boxerFocus.transform);
    }

    private void FindNearestOponent()
    {
        var candidate = _boxerFocusList
            .Where(e => e != null)
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude)
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
}
