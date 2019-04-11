using UnityEngine;

public interface IInteractable 
{
    GameObject GameObject { get; }
    Collider Collider { get; }

    void OnInteractionStart();
    void OnInteractionStop();
}
