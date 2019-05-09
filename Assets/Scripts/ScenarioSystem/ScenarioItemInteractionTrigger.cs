using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ServiceLocatorNamespace;

[RequireComponent(typeof(InteractableItem))]
public class ScenarioItemInteractionTrigger : MonoBehaviour
{
    [SerializeField, ScenarioFlag] private int interactionStartFlag;
    [SerializeField, ScenarioFlag] private int interactionStopFlag;

    [SerializeField, HideInInspector] private InteractableItem item;

    private ScenarioFlagsService service;

    private void OnValidate()
    {
        item = GetComponent<InteractableItem>();
    }

    private void OnEnable()
    {
        service = ServiceLocator.Instance.Get<ScenarioFlagsService>() as ScenarioFlagsService;

        item.InteractionStart += TriggerInteractionStartEvent;
        item.InteractionStop += TriggerInteractionStopEvent;
    }

    private void OnDisable()
    {
        item.InteractionStart -= TriggerInteractionStartEvent;
        item.InteractionStop -= TriggerInteractionStopEvent;
    }

    private void TriggerInteractionStartEvent()
    {
        if (interactionStartFlag != ScenarioFlag.None)
        {
            service.AddFlag(interactionStartFlag);
        }
    }

    private void TriggerInteractionStopEvent()
    {
        if (interactionStopFlag != ScenarioFlag.None)
        {
            service.AddFlag(interactionStopFlag);
        }
    }
}
