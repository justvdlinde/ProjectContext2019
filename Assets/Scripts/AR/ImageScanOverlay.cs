using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for handling the showing and hiding of the AR scan overlay, listens to <see cref="ImageTrackingController"/>
/// </summary>
public class ImageScanOverlay : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ImageTrackingController trackingController;

    [Header("Fields")]
    [SerializeField] private GameObject image;

    private void Start()
    {
        ToggleOverlay(true);
    }

    public void ToggleOverlay(bool show)
    {
        image.SetActive(show);
    }
}
