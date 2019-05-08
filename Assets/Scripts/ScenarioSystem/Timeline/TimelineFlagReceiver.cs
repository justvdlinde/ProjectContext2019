using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using ServiceLocatorNamespace;

public class TimelineFlagReceiver : MonoBehaviour, INotificationReceiver
{
    private ScenarioFlagsService flagService;

    private void Start()
    {
        flagService = ServiceLocator.Instance.Get<ScenarioFlagsService>() as ScenarioFlagsService;
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is TimelineFlagMarker)
        {
            flagService.AddFlag((notification as TimelineFlagMarker).ScenarioFlag);
        }
    }
}
