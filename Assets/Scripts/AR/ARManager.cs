using GoogleARCore;
using ServiceLocatorNamespace;
using UnityEngine;

public class ARManager : MonoBehaviour, IService
{
    [SerializeField] private ARCoreSession session;
    [SerializeField] private ImageTrackingController sessionController;
    [SerializeField] private ARCoreBackgroundRenderer backgroundRenderer;

    public void EnableAR(bool enable)
    {
        session.enabled = enable;
        sessionController.enabled = enable;

        if (enable)
        {
            backgroundRenderer.m_BackgroundRenderer.mode = UnityEngine.XR.ARRenderMode.MaterialAsBackground;
        }
    }

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService(this);
    }
}
