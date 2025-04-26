using System.Collections;
using UnityEngine;

public class FameRate : MonoBehaviour
{
    [Header("Frame Settings")]
    public int MaxRate = 9999;
    public float TargetFrameRate = 60.0f;

    private static FameRate _instance;
    private float currentFrameTime;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = MaxRate;
            currentFrameTime = Time.realtimeSinceStartup;
            StartCoroutine(WaitForNextFrame());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            currentFrameTime += 1.0f / TargetFrameRate;

            float elapsedTime = Time.realtimeSinceStartup;
            float sleepTime = currentFrameTime - elapsedTime - 0.01f;

            if (sleepTime > 0)
            {
                yield return new WaitForSecondsRealtime(sleepTime); 
            }

            while (elapsedTime < currentFrameTime)
            {
                elapsedTime = Time.realtimeSinceStartup;
                yield return null;
            }
        }
    }
}
