using UnityEngine;

public class Location : MonoBehaviour, IInteractable
{
    [SerializeField] [LocationID] private int id;
    public int ID => id;

    public GameObject GameObject => gameObject;
    public Collider Collider => GetComponent<Collider>();
    public bool HideAtStart => false;
    public bool DestroyAfterInteraction => false;

    public void OnInteractionStart()
    {
        OnSelectChange(true);
    }

    public void OnInteractionStop()
    {
        OnSelectChange(false);
    }

    private void OnSelectChange(bool selected)
    {
        // set outline color
    }
}
