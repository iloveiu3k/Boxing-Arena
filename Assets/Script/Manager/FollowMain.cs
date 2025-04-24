using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMain : MonoBehaviour
{
    private GameObject _player;

    private float speed = 1.2f;
    void Start()
    {
        _player = Player.Instance.gameObject;
    }
    public Vector3 direction = Vector3.forward;

    void Update()
    {
        Vector3 dir = (_player.transform.position-transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }
}