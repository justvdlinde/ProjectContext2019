using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedImageObject : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ImageTrackingController trackingController;

    [Header("Fields")]
    [SerializeField] private Rooms areaType;

    public AugmentedImage Image { get; private set; }

    public bool IsBeingTracked => isBeingTracked;
    private bool isBeingTracked;

    //private void Start()
    //{
    //    Hide(null);
    //}

    //private void OnEnable()
    //{
    //    trackingController.ImageTrackingFoundEvent += OnImageTrackingFoundEvent;
    //    trackingController.ImageTrackingLostEvent += OnImageTrackingLostEvent;
    //}

    //private void OnDisable()
    //{
    //    trackingController.ImageTrackingFoundEvent -= OnImageTrackingFoundEvent;
    //    trackingController.ImageTrackingLostEvent -= OnImageTrackingLostEvent;
    //}

    private void Start()
    {
        Hide();
    }

    public void SetImage(AugmentedImage image)
    {
        Image = image;
    }

    public void Update()
    {
        if (Image != null && Image.TrackingState == TrackingState.Tracking)
        {
            transform.localPosition = Image.CenterPose.position;
        }
    }

    //private void OnImageTrackingFoundEvent(TrackedImageObject trackedObject)
    //{
    //    // if areaTyoe == this.areaType, do Show of Hide
    //}

    //private void OnImageTrackingLostEvent(TrackedImageObject trackedObject)
    //{

    //}

    public void Show()
    {
        gameObject.SetActive(true);
        enabled = true;
        isBeingTracked = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        enabled = false;
        isBeingTracked = false;
    }
}
