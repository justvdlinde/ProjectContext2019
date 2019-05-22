using ServiceLocatorNamespace;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerService : MonoBehaviour, MonoService
{
    [SerializeField] private GameObject root;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        ServiceLocator.Instance.AddService(this);

        ShowLoadingbar(false);
    }

    public void LoadScene(string scene, Action onDone = null)
    {
        ShowLoadingbar(true);
        onDone += () => ShowLoadingbar(false);
        StartCoroutine(SceneManagerUtility.LoadScene(scene, onDone));
    }

    public void ShowLoadingbar(bool show)
    {
        enabled = show;
        root.SetActive(show);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService(this);
    }
}
