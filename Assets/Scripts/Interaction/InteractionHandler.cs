using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manager class for handling input and detecting <see cref="IInteractable"/> objects
/// </summary>
public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private float rayDistance;

    private RaycastHit hit;
    private Ray ray;
    private Vector2 touchOrigin;

    public Action<IInteractable> InteractedWithObjectEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    public void SetActive(bool active)
    {
        enabled = active;
    }

    private void OnClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, rayDistance))
        {
            IInteractable obj = hit.transform.gameObject.GetInterface<IInteractable>();
            if (obj != null)
            {
                InteractableObjectHit(obj);
            }
        }
    }

    private void InteractableObjectHit(IInteractable interactable)
    {
        InteractedWithObjectEvent?.Invoke(interactable);
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayDistance, Color.red);
    }
}
