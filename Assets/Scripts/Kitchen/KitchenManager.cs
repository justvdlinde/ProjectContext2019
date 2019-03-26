using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private KitchenItemAmount[] neededItems;
    public KitchenItemAmount[] NeededItems => neededItems;

    public Dictionary<KitchenItemObject, int> collectedItemsAmountPair = new Dictionary<KitchenItemObject, int>();

    [SerializeField] private Trigger trigger;
    
    private void OnEnable()
    {
        trigger.TriggerEnterEvent += OnTriggerEnterEvent;
    }

    private void OnDisable()
    {
        trigger.TriggerEnterEvent -= OnTriggerEnterEvent;
    }

    private void OnTriggerEnterEvent(Collider collider)
    {
        KitchenItemPickup pickup = collider.GetComponent<KitchenItemPickup>();
        if(pickup != null)
        {
            AddItem(pickup);
            pickup.Collect();
        }
    }

    public void AddItem(KitchenItemPickup item)
    {
        KitchenItemObject obj = item.InventoryObject;

        if (collectedItemsAmountPair.ContainsKey(obj))
        {
            collectedItemsAmountPair[obj]++;
        }
        else
        {
            collectedItemsAmountPair.Add(obj, 1);
        }

        //foreach (KeyValuePair<KitchenItemObject, int> kvp in collectedItemsAmountPair)
        //{
        //    Debug.Log(kvp.Key.item + " amount " + kvp.Value);
        //}
    }
}

[System.Serializable]
public class KitchenItemAmount
{
    public KitchenItemObject item;
    public int amount;
}
