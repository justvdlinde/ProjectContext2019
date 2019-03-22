using System;
using UnityEngine;

public class ScreenInput : MonoBehaviour
{
    [SerializeField] private float touchLength = 1;

    private Vector2 touchOrigin;
    private RaycastHit hit;

    public Action<ILevitatable> ObjectSelectedEvent;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, touchLength))
            {
                ILevitatable obj = hit.transform.gameObject.GetInterface<ILevitatable>();
                if (obj != null)
                {
                    LevitatablebjectHit(obj);
                }
            }
        }
    }

    private void LevitatablebjectHit(ILevitatable interactable)
    {
        ObjectSelectedEvent?.Invoke(interactable);
    }
}
