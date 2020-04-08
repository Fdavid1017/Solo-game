using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public List<GameObject> objectsToDisable = new List<GameObject>();

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsynchrously(sceneIndex));
    }

    IEnumerator LoadSceneAsynchrously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        foreach (var item in objectsToDisable)
        {
            item.SetActive(false);
        }

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            yield return null;
        }
    }
}
