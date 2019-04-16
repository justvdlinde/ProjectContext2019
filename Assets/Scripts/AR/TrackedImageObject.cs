using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedImageObject : MonoBehaviour
{
    public AugmentedImage Image;

    [SerializeField] private GameObject objectPrefab;

    private GameObject objectInstance;

    private void Start()
    {
        objectInstance = Instantiate(objectPrefab);
        objectInstance.SetActive(false);
    }

    public void Update()
    {
        if (Image == null || Image.TrackingState != TrackingState.Tracking)
        {
            objectInstance.SetActive(false);
            return;
        }

        objectInstance.transform.localPosition = Image.CenterPose.position;
    }
}
