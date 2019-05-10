using UnityEngine;

public interface IInteractable 
{
    GameObject GameObject { get; }
    Collider Collider { get; }

    bool HideAtStart { get; }
    bool DestroyAfterInteraction { get; }

    void OnInteractionStart();
    void OnInteractionStop();
}
