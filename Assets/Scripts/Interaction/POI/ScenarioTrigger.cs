using UnityEngine;

public class ScenarioTrigger : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get { return gObject; } }
    public Collider Collider { get { return collider; } }

    [SerializeField, HideInInspector] private GameObject gObject;
    [SerializeField, HideInInspector] private new Collider collider;

    private void OnValidate()
    {
        gObject = gameObject;
        collider = GetComponent<Collider>();
    }

    public void OnInteractionStart()
    {
        collider.enabled = false;
    }

    public void OnInteractionStop()
    {
        collider.enabled = false;
    }
}
