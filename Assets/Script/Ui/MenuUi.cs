using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MenuUi : Singleton<MenuUi>
{
    [SerializeField] List<Button> _leaguesButton;
    [SerializeField] List<Button> _enemyButton;
    [SerializeField] Button _riseDifficult;
    [SerializeField] Button _fallDifficult;
    [SerializeField] Button _startGame ;
    private int _diff = 1;
    private TMP_Text _tmpDiff;
    
    private void Start()
    {
        _riseDifficult.onClick.AddListener(Rise);
        _fallDifficult.onClick.AddListener(Fall);
        _startGame.onClick.AddListener(StartGame);
        _tmpDiff = _startGame.GetComponentInChildren<TMP_Text>();
        _tmpDiff.text = _diff.ToString();
    }
    private void Rise()
    {
        if (_diff < 10)
        {
            _diff++;
            _tmpDiff.text = _diff.ToString();
        }
    }

    private void Fall()
    {
        if (_diff > 1)
        {
            _diff--;
            _tmpDiff.text = _diff.ToString();
        }
    }
    private void StartGame()
    {
        DataScene.Instance.numberLeague = _leaguesButton.Count(btn => btn.GetComponent<ButtonAddBoxer>().hasChoice == true);
        DataScene.Instance.numberEnemy = _enemyButton.Count(btn => btn.GetComponent<ButtonAddBoxer>().hasChoice == true);
        DataScene.Instance.difficultLevel = _diff;
        DataScene.Instance.AsyncLoader.LoadLevel("Match");
    
    }
}
