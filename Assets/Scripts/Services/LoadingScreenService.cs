using ServiceLocatorNamespace;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenService : MonoBehaviour, MonoService
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject root;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        ServiceLocator.Instance.AddService(this);

        Show(false);
    }

    public void Update()
    {
        slider.value = Mathf.Lerp(0, SceneManagerUtility.Progress, Time.deltaTime * 2);
    }

    public void Show(bool show)
    {
        Debug.Log("show " + show);

        root.SetActive(show);
        enabled = show;
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService(this);
    }
}
