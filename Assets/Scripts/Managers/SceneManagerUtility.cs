using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerUtility 
{
    public static float Progress { get; private set; }

    public static IEnumerator LoadScene(string scene, Action onDoneLoading = null)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            Progress = asyncOperation.progress;
            yield return null;
        }

        onDoneLoading?.Invoke();
    }
}
