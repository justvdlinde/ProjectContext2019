using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIView : POIBehavior
{
    protected override void Start()
    {
        base.Start();
        if (!poiContainer.ContainsKey(uniqueId)) poiContainer.Add(uniqueId, this);

        if (requiredToPlay.currentStatus == status.Completed) {

        }
    }
}
