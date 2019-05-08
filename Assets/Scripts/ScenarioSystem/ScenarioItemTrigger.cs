using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ServiceLocatorNamespace;

[RequireComponent(typeof(InteractableItem))]
public class ScenarioItemTrigger : MonoBehaviour
{
    [SerializeField, ScenarioFlag] private int flag;
    [SerializeField, HideInInspector] private InteractableItem item;

    private ScenarioFlagsService service;

    private void OnValidate()
    {
        item = GetComponent<InteractableItem>();
    }

    private void OnEnable()
    {
        service = ServiceLocator.Instance.Get<ScenarioFlagsService>() as ScenarioFlagsService;

        item.InteractionStart += TriggerScenarioEvent;
    }

    private void OnDisable()
    {
        item.InteractionStart -= TriggerScenarioEvent;
    }

    private void TriggerScenarioEvent()
    {
        service.AddFlag(flag);
    }
}
