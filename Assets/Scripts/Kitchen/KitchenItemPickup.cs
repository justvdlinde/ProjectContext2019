using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class KitchenItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenItemObject inventoryObject;
    public KitchenItemObject InventoryObject => inventoryObject;

    private void OnValidate()
    {
        name = "Item: " + inventoryObject.item.ToString();
    }

    public void Interact()
    {
        Destroy(gameObject);
    }
}
