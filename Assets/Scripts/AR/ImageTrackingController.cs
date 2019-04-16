using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using System;

/// <summary>
/// Controller for tracking AR images. Calls <see cref="ImageTrackingFoundEvent"/> and <see cref="ImageTrackingLostEvent"/> when images are being tracked or when lost
/// </summary>
public class ImageTrackingController : MonoBehaviour
{
    public Action<TrackedImageObject> ImageTrackingFoundEvent;
    public Action<TrackedImageObject> ImageTrackingLostEvent;

    /// <summary>
    /// A prefab for visualizing an AugmentedImage.
    /// </summary>
    [SerializeField] private TrackedImageObject objectPrefab;

    private Dictionary<int, TrackedImageObject> trackedObjects = new Dictionary<int, TrackedImageObject>();
    private List<AugmentedImage> scannableImages = new List<AugmentedImage>();

    private bool isTrackingImage;

    public void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        Session.GetTrackables(scannableImages, TrackableQueryFilter.Updated);

        foreach (var image in scannableImages)
        {
            TrackedImageObject visualizer = null;
            trackedObjects.TryGetValue(image.DatabaseIndex, out visualizer);

            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                if(!isTrackingImage)
                {
                    ImageTrackingFoundEvent?.Invoke(visualizer);
                }

                TrackImage(image, visualizer);
                isTrackingImage = true;
            }
            else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                RemoveTrackedImage(image, visualizer);

                if(isTrackingImage)
                {
                    ImageTrackingLostEvent?.Invoke(visualizer);
                }

                isTrackingImage = false;
            }
        }
    }

    private void TrackImage(AugmentedImage image, TrackedImageObject visualizer)
    {
        Anchor anchor = image.CreateAnchor(image.CenterPose);
        visualizer = Instantiate(objectPrefab, anchor.transform);
        visualizer.Image = image;
        trackedObjects.Add(image.DatabaseIndex, visualizer);
    }

    private void RemoveTrackedImage(AugmentedImage image, TrackedImageObject visualizer)
    {
        trackedObjects.Remove(image.DatabaseIndex);
        Destroy(visualizer.gameObject);
    }
}

