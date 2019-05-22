using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerUtility 
{
    public static float Progress { get; private set; }

    public static IEnumerator LoadScene(string scene, Action onDoneLoading = null)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            Progress = asyncOperation.progress;
            yield return asyncOperation;
        }

        Progress = 1f;
        onDoneLoading?.Invoke();
    }
}
