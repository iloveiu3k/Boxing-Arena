using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake() {
        if (Instance == null){
            Instance = this as T;
        }
        else
        {
            Debug.LogWarning("Duplicate object. Destroy object name: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
