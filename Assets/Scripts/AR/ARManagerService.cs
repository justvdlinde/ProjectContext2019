using GoogleARCore;
using ServiceLocatorNamespace;
using System;
using UnityEngine;

public class ARManagerService : MonoBehaviour, MonoService
{
    public Action<TrackedImageObject> NewImageTrackedEvent;

    [SerializeField] private GameObject root;
    [SerializeField] private ARCoreSession session;
    [SerializeField] private ImageTrackingController sessionController;
    [SerializeField] private ARCoreBackgroundRenderer backgroundRenderer;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void EnableAR(bool enable)
    {
#if !UNITY_EDITOR
        root.gameObject.SetActive(enable);

        session.enabled = enable;
        sessionController.enabled = enable;

        if (enable)
        {
            backgroundRenderer.m_BackgroundRenderer.mode = UnityEngine.XR.ARRenderMode.MaterialAsBackground;
        }
#endif
    }

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
        Debug.Log("added service " + this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService(this);
    }
}
