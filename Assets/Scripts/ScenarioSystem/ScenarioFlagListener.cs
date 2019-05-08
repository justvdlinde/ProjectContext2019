using ServiceLocatorNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioFlagListener : MonoBehaviour
{
    [SerializeField, ScenarioFlag] private int requiredFlag;

    private ScenarioFlagsService flagService;

    [SerializeField] private UnityEvent onFlagAddedEvent;

    private void Start()
    {
        flagService = ServiceLocator.Instance.Get<ScenarioFlagsService>() as ScenarioFlagsService;
        flagService.FlagAdded += OnFlagAdded;
    }

    private void OnDestroy()
    {
        flagService.FlagAdded -= OnFlagAdded;
    }

    private void OnFlagAdded(ScenarioFlag flag)
    {
        if(flag.Equals(this.requiredFlag))
        {
            onFlagAddedEvent.Invoke();
        }
    }
}
