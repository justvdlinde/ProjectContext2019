using GoogleARCore;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for tracking AR images.
/// </summary>
public class ImageTrackingController : MonoBehaviour
{
    [SerializeField] private ARCoreBackgroundHandler arBackgroundHandler;
    [SerializeField] private List<TrackedImageObject> trackableObjects;

    private ARManagerService arManager;

    private Dictionary<int, TrackedImageObject> trackedObjects = new Dictionary<int, TrackedImageObject>();
    private List<AugmentedImage> scannableImages = new List<AugmentedImage>();

    private void OnValidate()
    {
        arBackgroundHandler = FindObjectOfType<ARCoreBackgroundHandler>();
    }

    private void Start()
    {
        arManager = ServiceLocatorNamespace.ServiceLocator.Instance.Get<ARManagerService>() as ARManagerService;
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
            //Debug.Log("image id: " + image.DatabaseIndex + " image name " + image.Name + " tracking state " + image.TrackingState);
            TrackedImageObject trackedObject = trackableObjects[image.DatabaseIndex];

            if (trackedObject == null)
            {
                Debug.LogError("No TrackedImageObject found for id " + image.DatabaseIndex);
                return;
            }

            if (image.TrackingState == TrackingState.Tracking && !trackedObject.IsBeingTracked)
            {
                //Debug.Log("found: " + image.DatabaseIndex);
                ImageTrackingFound(image, trackedObject);
            }
            else if (image.TrackingState == TrackingState.Stopped && trackedObject.IsBeingTracked)
            {
                //Debug.Log("lost: " + image.DatabaseIndex);
                ImageTrackingLost(image, trackedObject);
            }
        }
    }

    private void ImageTrackingFound(AugmentedImage image, TrackedImageObject trackedImage)
    {
        Debug.Log("OnImageTrackingFound() " + image.DatabaseIndex);

        Anchor anchor = image.CreateAnchor(image.CenterPose);
        trackedImage.Show(image);
        arBackgroundHandler.ShowBackgroundCamera(false);

        arManager.NewImageTrackedEvent?.Invoke(trackedImage);
    }

    private void ImageTrackingLost(AugmentedImage image, TrackedImageObject trackedImage)
    {
        Debug.Log("OnImageTrackingLost() " + image.DatabaseIndex);

        arBackgroundHandler.ShowBackgroundCamera(true);
        trackedImage.Hide();
    }
}

