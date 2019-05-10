using UnityEngine;

public class ScenarioTrigger : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get { return gObject; } }
    public Collider Collider { get { return collider; } }

    public bool HideAtStart => hideAtStart;
    [SerializeField] private bool hideAtStart;

    public bool DestroyAfterInteraction => destroyAfterInteraction;
    [SerializeField] private bool destroyAfterInteraction;

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
