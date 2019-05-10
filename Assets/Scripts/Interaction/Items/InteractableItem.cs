using System;
using UnityEngine;

/// <summary>
/// Component for items that are interactable. Requires a collider
/// </summary>
[RequireComponent(typeof(Collider))]
public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField][ItemID] private int id;
    public int ID => id;

    public GameObject GameObject { get { return gObject; } }
    public Collider Collider { get { return collider; } }

    public Action InteractionStart;
    public Action InteractionStop;

    public bool HideAtStart => hideAtStart;
    [SerializeField] private bool hideAtStart;

    bool IInteractable.DestroyAfterInteraction => destroyAfterInteraction;
    [SerializeField] private bool destroyAfterInteraction;

    [SerializeField, HideInInspector] private GameObject gObject;
    [SerializeField, HideInInspector] private new Collider collider;

    private new Rigidbody rigidbody;
    private bool colliderWasEnabledBeforeInteraction;
    private bool rigidbodyWasKinematicBeforeInteraction;

    private void OnValidate()
    {
        gObject = gameObject;
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Show(!hideAtStart);
    }

    public void OnInteractionStart()
    {
        colliderWasEnabledBeforeInteraction = collider.enabled;
        collider.enabled = false;

        if(rigidbody != null)
        {
            rigidbodyWasKinematicBeforeInteraction = rigidbody.isKinematic;
            rigidbody.isKinematic = true;
        }

        InteractionStart?.Invoke();
    }
    
    public void OnInteractionStop()
    {
        InteractionStop?.Invoke();

        if (destroyAfterInteraction)
        {
            Destroy(gameObject);
            return;
        }

        collider.enabled = colliderWasEnabledBeforeInteraction;
        if (rigidbody != null)
        {
            rigidbody.isKinematic = rigidbodyWasKinematicBeforeInteraction;
        }
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public void DestroyAfterInteraction(bool destroy)
    {
        destroyAfterInteraction = destroy;
    }
}
