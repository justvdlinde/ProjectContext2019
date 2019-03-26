using System;
using UnityEngine;

public class ScreenInput : MonoBehaviour
{
    [SerializeField] private float touchLength = 1;

    private Vector2 touchOrigin;
    private RaycastHit hit;

    public Action<ICollectable> InteractedWithObjectEvent;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, touchLength))
            {
                ICollectable interactable = hit.transform.gameObject.GetInterface<ICollectable>();
                if (interactable != null)
                {
                    InteractableObjectHit(interactable);
                }
            }
        }
    }

    private void InteractableObjectHit(ICollectable interactable)
    {
        InteractedWithObjectEvent?.Invoke(interactable);
    }
}
