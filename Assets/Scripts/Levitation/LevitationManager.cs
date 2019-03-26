using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationManager : MonoBehaviour
{
    [SerializeField] private Transform carryTarget;
    [SerializeField] private float rayDistance;
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotationSpeed;

    private bool isCarrying;
    private ILevitatable carriedObject;
    private Vector2 touchOrigin;
    private RaycastHit pickupHit;
    private Ray pickupRay;

    private void Update()
    {
        if (isCarrying)
        {
            // TODO: if object is hindered, Drop();

            Carry(carriedObject);

            if (Input.GetMouseButtonDown(0))
            {
                Drop();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                pickupRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(pickupRay, out pickupHit, rayDistance))
                {
                    ILevitatable obj = pickupHit.transform.gameObject.GetInterface<ILevitatable>();
                    if (obj != null)
                    {
                        Pickup(obj);
                    }
                }
            }
        }
    }

    private void Pickup(ILevitatable gObject)
    {
        carriedObject = gObject;
        isCarrying = true;
        gObject.DestroyEvent += OnCarriedObjectDestroyEvent;
        gObject.OnLevitateStart(this);

        carriedObject.Rigidbody.useGravity = false;
        carriedObject.Rigidbody.isKinematic = true;
    }

    private void Carry(ILevitatable gObject)
    {
        Vector3 pos = Vector3.Lerp(gObject.Rigidbody.transform.position, carryTarget.position, Time.deltaTime * followSpeed);
        gObject.Rigidbody.MovePosition(pos);

        gObject.Rigidbody.transform.position = Vector3.Lerp(gObject.Rigidbody.transform.position, carryTarget.position, Time.deltaTime * followSpeed);
        gObject.Rigidbody.transform.rotation = Quaternion.Slerp(gObject.Rigidbody.transform.rotation, carryTarget.rotation, Time.deltaTime * followSpeed);
    }

    private bool CarriedObjectIsHindered()
    {
        RaycastHit hit;
        return !Physics.Raycast(carryTarget.position, carryTarget.position - carriedObject.Rigidbody.position, out hit);
    }

    private void Drop()
    {
        carriedObject.DestroyEvent -= OnCarriedObjectDestroyEvent;
        carriedObject.OnLevitateStop(this);

        carriedObject.Rigidbody.useGravity = true;
        carriedObject.Rigidbody.isKinematic = false;

        isCarrying = false;
        carriedObject = null;
    }

    private void OnCarriedObjectDestroyEvent()
    {
        Drop();
    }
}
