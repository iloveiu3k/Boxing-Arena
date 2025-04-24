using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class UnitsManager : Singleton<UnitsManager>
{
    // Start is called before the first frame update    
    public List<GameObject> _enemyList;
    public List<GameObject> EnemyList { get => _enemyList; }
    public List<GameObject> _leagueList;
    public List<GameObject> LeagueList { get => _leagueList; }

    public static Action OnLoadDataBoxer;

    protected override void Awake()
    {
        base.Awake();
        _enemyList = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        _leagueList = GameObject.FindGameObjectsWithTag("League").ToList();
        _leagueList.Add(GameObject.FindWithTag("Player"));

        _enemyList.Select(go => go.GetComponent<BoxerFocus>())
                .Where(ai => ai != null)
                .ToList()
                .ForEach(ai => 
                {
                    ai.SetBoxerFocusList(LeagueList);
                });

        _leagueList.Select(go => go.GetComponent<BoxerFocus>())
                .Where(ai => ai != null)
                .ToList()
                .ForEach(ai => 
                {
                    ai.SetBoxerFocusList(EnemyList);
                });
    }
}
