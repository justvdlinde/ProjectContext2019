using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class KitchenItemPickup : LevitationObject, ICollectable
{
    [SerializeField] private KitchenItemObject inventoryObject;
    public KitchenItemObject InventoryObject => inventoryObject;

    protected override void OnValidate()
    {
        base.OnValidate();

        name = "Item: " + inventoryObject.item.ToString();
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}
