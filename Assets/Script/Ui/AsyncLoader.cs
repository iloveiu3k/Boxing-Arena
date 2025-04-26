using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingSlider;

    public void LoadLevelSelection()
    {
        LoadLevelScreens();
    }

    public void LoadLevel(string sceneName)
    {
        LoadLevelScreens(sceneName);
    }

    //* Default scene name is LevelSelection
    private void LoadLevelScreens(string sceneName = "Match")
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    //* Update the loading slider
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // _loadingSlider.fillAmount = asyncOperation.progress;

            // var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            // _loadingSlider.fillAmount = progress;

            yield return null;
        }

        _loadingScreen.SetActive(false);
    }
}
