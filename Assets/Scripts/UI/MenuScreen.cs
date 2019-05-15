﻿using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ARManager arManager;

    [Header("UI")]
    [SerializeField] private Button openScanButton;
    [SerializeField] private Button cancelScanButton;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject scanOverlayImage;

    private void OnEnable()
    {
        openScanButton.onClick.AddListener(OnScanButtonPressed);
        cancelScanButton.onClick.AddListener(OnCancelButtonPressed);
    }

    private void OnDisable()
    {
        openScanButton.onClick.RemoveListener(OnScanButtonPressed);
        cancelScanButton.onClick.RemoveListener(OnCancelButtonPressed);
    }

    private void OnScanButtonPressed()
    {
        ShowMenu(false);
        arManager.EnableAR(true);
    }

    private void OnCancelButtonPressed()
    {
        ShowMenu(true);
        arManager.EnableAR(false);
    }
    
    public void ShowMenu(bool show)
    {
        menuUI.SetActive(!show);
        scanOverlayImage.SetActive(show);
    }
}
