using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataScene : MonoBehaviour
{
    public static DataScene Instance;

    public int numberLeague;
    public int numberEnemy;
    public int difficultLevel;
    public AsyncLoader AsyncLoader { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        AsyncLoader = GetComponentInChildren<AsyncLoader>();

    }
}

