using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIBehavior : MonoBehaviour
{
    protected POIStatus currentStatus;
    protected Dictionary<string, POIView> poiContainer;
    protected POIView requiredToPlay = null;

    protected virtual void Start()
    {
        
    }
}
