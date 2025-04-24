using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _difficultLevel;
    [SerializeField] private Collider _areaBox; 
    protected override void Awake()
    {
        base.Awake();
    }
    public int GetDifficultLevel()
    {
        return _difficultLevel;
    }
    public Bounds GetAreaBox()
    {
        return _areaBox.bounds;
    }
}
