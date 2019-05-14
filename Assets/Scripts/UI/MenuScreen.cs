using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button openScanButton;
    [SerializeField] private Button cancelScanButton;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject scanOverlayImage;

    [Header("AR")]
    [SerializeField] private ARCoreSession session;
    [SerializeField] private ImageTrackingController sessionController;
    [SerializeField] private ARCoreBackgroundRenderer backgroundRenderer;

    private void OnEnable()
    {
        openScanButton.onClick.AddListener(() => EnableAR(true));
        cancelScanButton.onClick.AddListener(() => EnableAR(false));
    }

    private void OnDisable()
    {
        openScanButton.onClick.RemoveListener(() => EnableAR(true));
        cancelScanButton.onClick.RemoveListener(() => EnableAR(false));
    }

    private void EnableAR(bool enable)
    {
        Debug.Log("show scanner " + enable);

        session.enabled = enable;
        sessionController.enabled = enable;

        if (enable)
        {
            backgroundRenderer.m_BackgroundRenderer.mode = UnityEngine.XR.ARRenderMode.MaterialAsBackground;
        }

        menuUI.SetActive(!enable);
        scanOverlayImage.SetActive(enable);
    }
}
