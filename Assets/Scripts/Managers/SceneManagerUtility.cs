using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ServiceLocatorNamespace;

public static class SceneManagerUtility 
{
    public static float Progress { get; private set; }

    private static LoadingScreenService loadingScreenService;

    public static IEnumerator LoadScene(string scene, Action onDoneLoading = null)
    {
        if(loadingScreenService == null)
        {
            loadingScreenService = ServiceLocator.Instance.Get<LoadingScreenService>() as LoadingScreenService;
        }
        loadingScreenService.Show(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            Progress = asyncOperation.progress;

            if(asyncOperation.progress >= 0.9f)
            {
                Debug.Log("Done loading");
                Progress = 1f;
                loadingScreenService.Show(false);
                onDoneLoading?.Invoke();
            }

            yield return asyncOperation;
        }
    }
}
