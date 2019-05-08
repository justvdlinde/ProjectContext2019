using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineFlagMarker : Marker, INotification, INotificationOptionProvider
{
    [SerializeField, ScenarioFlag] private int scenarioFlag;
    public int ScenarioFlag => scenarioFlag;

    public PropertyName id { get { return new PropertyName(); } }

    public NotificationFlags flags => NotificationFlags.TriggerOnce;
}

