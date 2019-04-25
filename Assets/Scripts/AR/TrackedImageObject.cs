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
    [SerializeField] private int imageID;

    public AugmentedImage Image { get; private set; }
    public bool IsBeingTracked { get; private set; }

    private void Start()
    {
        Hide();
    }

    public void Update()
    {
        if (Image != null)        
        {
            if (Image.TrackingState == TrackingState.Tracking)
            {
                transform.localPosition = Image.CenterPose.position;
            }
            else
            {
                Hide();
            }
        }
    }

    private void OnImageTrackingFoundEvent(AugmentedImage image)
    {
        if(image.DatabaseIndex == imageID)
        {
            Show(image);
        }
    }

    public void Show(AugmentedImage image)
    {
        Image = image;
        gameObject.SetActive(true);
        enabled = true;
        IsBeingTracked = true;
    }

    public void Hide()
    {
        Image = null;
        gameObject.SetActive(false);
        enabled = false;
        IsBeingTracked = false;
    }
}
