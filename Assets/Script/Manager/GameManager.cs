using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _difficultLevel;
    [SerializeField] private Collider _areaBox; 
    [SerializeField] private List<Transform> _positionEnemys;
    [SerializeField] private List<Transform> _positionLeagues;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _league;
    public static Action OnWiner;
    private int numberOfEnemy = 1;
    private Animator _animator;
    protected override void Awake()
    {
        base.Awake();
        SpawnUnits();
    }
    private void Start()
    {
        _animator = UIController.Instance.GetComponent<Animator>();
        PlayerStats.OnDying += OnLoser;
    }
    private void OnDisable()
    {
        PlayerStats.OnDying -= OnLoser;
    }
    public int GetDifficultLevel()
    {
        return _difficultLevel;
    }
    public Bounds GetAreaBox()
    {
        return _areaBox.bounds;
    }

    public void SpawnUnits()
    {
        for (int i = 0;i < DataScene.Instance.numberEnemy;i++)
        {
            Instantiate(_enemy,_positionEnemys[i].position,Quaternion.identity);
        }
        for (int i = 0;i < DataScene.Instance.numberLeague;i++)
        {
            Instantiate(_league,_positionLeagues[i].position,Quaternion.identity);
        }
        _difficultLevel = DataScene.Instance.difficultLevel;
        numberOfEnemy += DataScene.Instance.numberEnemy;
    }
    public void FallNumberEnemyDying()
    {
        numberOfEnemy --;
        if(numberOfEnemy <=0)
        {
            OnWiner?.Invoke();
            StartCoroutine(Winner());
        }
    }
    private IEnumerator Winner()
    {
        yield return new WaitForSeconds(4);
        _animator.SetTrigger("Winner");
        StartCoroutine(WaitToRunMenu(6));
    }
    public void OnLoser()
    {
        if (_animator != null && _animator.isActiveAndEnabled)
        {
            _animator.SetTrigger("Loser");
            StartCoroutine(WaitToRunMenu(4));
        }
    }
    private IEnumerator WaitToRunMenu(float time)
    {
        yield return new WaitForSeconds(time);
        DataScene.Instance.AsyncLoader.LoadLevel("Menu");
    }
}
