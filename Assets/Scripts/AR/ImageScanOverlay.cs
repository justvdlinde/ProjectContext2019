using GoogleARCore.Examples.AugmentedImage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for handling the showing and hiding of the AR scan overlay, listens to <see cref="ImageTrackingController"/>
/// </summary>
public class ImageScanOverlay : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ImageTrackingController trackingController;

    [Header("Fields")]
    [SerializeField] private GameObject image;

    private Action<TrackedImageObject> OnImageTrackingFoundEvent;
    private Action<TrackedImageObject> OnImageTrackingLostEvent;

    private void Start()
    {
        ShowOverlay();

        OnImageTrackingFoundEvent = (TrackedImageObject v) => ShowOverlay();
        OnImageTrackingLostEvent = (TrackedImageObject v) => HideOverlay();
    }

    private void OnEnable()
    {
        trackingController.ImageTrackingFoundEvent += OnImageTrackingFoundEvent;
        trackingController.ImageTrackingLostEvent += OnImageTrackingLostEvent;
    }

    private void OnDisable()
    {
        trackingController.ImageTrackingFoundEvent -= OnImageTrackingFoundEvent;
        trackingController.ImageTrackingLostEvent -= OnImageTrackingLostEvent;
    }

    private void ShowOverlay()
    {
        image.SetActive(true);
    }

    private void HideOverlay()
    {
        image.SetActive(false);
    }
}
