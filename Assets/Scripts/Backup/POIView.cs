using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIView : POIBehavior
{
    private void Start()
    {
        if (!poiContainer.ContainsKey(uniqueId)) poiContainer.Add(uniqueId != string.Empty ? uniqueId : "", this);

        if (requiredToPlay?.currentStatus == POIStatus.Completed) {

        }
    }
}
