using GoogleARCore;
using UnityEngine;

public class ARManager : MonoBehaviour
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
}
