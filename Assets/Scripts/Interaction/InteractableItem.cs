using System;
using UnityEngine;

/// <summary>
/// Component for items that are interactable. Requires a collider
/// </summary>
[RequireComponent(typeof(Collider))]
public class InteractableItem : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get { return gObject; } }
    public Collider Collider { get { return collider; } }

    private GameObject gObject;
    private new Collider collider;

    private void OnValidate()
    {
        gObject = gameObject;
        collider = GetComponent<Collider>();
    }

    public void OnInteractionStart()
    {

    }
    
    public void OnInteractionStop()
    {

    }
}
