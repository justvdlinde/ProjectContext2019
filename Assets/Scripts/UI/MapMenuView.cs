using UnityEngine;
using UnityEngine.UI;

public class MapMenuView : MenuBehavior
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;

    [Header("Session")]
    [SerializeField] private GoogleARCore.ARCoreSession session;
    [SerializeField] private ImageTrackingController sessionController;
    [SerializeField] private GoogleARCore.ARCoreBackgroundRenderer backgroundRenderer;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(() => StartGame());
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(() => StartGame());
    }

    private void StartGame()
    {
        session.enabled = true;
        sessionController.enabled = true;
        menuBehavior.MapMenuView.SetActive(false);
        menuBehavior.SessionMenuView.SetActive(true);
        backgroundRenderer.m_BackgroundRenderer.mode = UnityEngine.XR.ARRenderMode.MaterialAsBackground;
    }

}
