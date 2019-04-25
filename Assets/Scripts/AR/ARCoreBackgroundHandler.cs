using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCoreBackgroundHandler : MonoBehaviour
{
    [SerializeField] private ARCoreBackgroundRenderer backgroundRenderer;

    private void OnValidate()
    {
        backgroundRenderer = GetComponent<ARCoreBackgroundRenderer>();
    }

    public void ShowBackgroundCamera(bool show)
    {
        backgroundRenderer.enabled = show;
    }
}
