using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public Action<Collider> TriggerEnterAction;

    [SerializeField] private UnityEvent triggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterAction?.Invoke(other);
        triggerEnterEvent.Invoke();
    }
}
