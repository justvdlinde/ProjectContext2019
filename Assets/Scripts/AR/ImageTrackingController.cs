using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using System;
using ServiceLocator;

/// <summary>
/// Controller for tracking AR images. Calls <see cref="ImageTrackingFoundEvent"/> and <see cref="ImageTrackingLostEvent"/> when images are being tracked or when lost
/// </summary>
public class ImageTrackingController : MonoBehaviour
{
    public Action<TrackedImageObject> ImageTrackingFoundEvent;
    public Action<TrackedImageObject> ImageTrackingLostEvent;

    [SerializeField] private ARCoreBackgroundHandler arBackgroundHandler;

    [SerializeField] private List<TrackedImageObject> trackableObjects;

    private Dictionary<int, TrackedImageObject> trackedObjects = new Dictionary<int, TrackedImageObject>();
    private List<AugmentedImage> scannableImages = new List<AugmentedImage>();

    private void Start()
    {
        for (int i = 0; i < trackableObjects.Count; i++)
        {
            trackedObjects.Add(i, trackableObjects[i]);
        }
    }

    private void OnValidate()
    {
        if (arBackgroundHandler == null)
        {
           arBackgroundHandler = FindObjectOfType<ARCoreBackgroundHandler>();
        }
    }

    public void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        Session.GetTrackables(scannableImages, TrackableQueryFilter.Updated);

        foreach (var image in scannableImages)
        {
            trackedObjects.TryGetValue(image.DatabaseIndex, out TrackedImageObject trackedObject);

            if (image.TrackingState == TrackingState.Tracking && !trackedObject.IsBeingTracked)
            {
                ImageTrackingFound(image, trackedObject);
            }
            else if (image.TrackingState == TrackingState.Stopped && trackedObject.IsBeingTracked)
            {
                ImageTrackingLost(image, trackedObject);
            }
        }
    }

    private void ImageTrackingFound(AugmentedImage image, TrackedImageObject trackedImage)
    {
        Debug.Log("OnImageTrackingFound()");

        Anchor anchor = image.CreateAnchor(image.CenterPose);

        trackedImage.SetImage(image);
        trackedImage.Show();

        ImageTrackingFoundEvent?.Invoke(trackedImage);
        arBackgroundHandler.ShowBackgroundCamera(false);
    }

    private void ImageTrackingLost(AugmentedImage image, TrackedImageObject trackedImage)
    {
        Debug.Log("OnImageTrackingLost()");

        ImageTrackingLostEvent?.Invoke(trackedImage);
        arBackgroundHandler.ShowBackgroundCamera(true);

        trackedImage.Hide();
    }
}

