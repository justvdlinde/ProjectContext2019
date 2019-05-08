using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ScenarioTrigger : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private new Collider collider;

    [SerializeField] private UnityEvent onEnter;

    private void OnValidate()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
    }

}