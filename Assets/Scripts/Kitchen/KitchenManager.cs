using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour
{
    public Action<KitchenItemObject> NewItemAddedEvent;
    public Action AcquiredAllNeededItemsEvent;

    [SerializeField] private KitchenItemAmount[] neededItems;
    [SerializeField] private Trigger trigger;

    public Dictionary<KitchenItemObject, int> collectedItemsDict = new Dictionary<KitchenItemObject, int>();
    public Dictionary<KitchenItemObject, int> NeededItemsDict { get; private set; }

    private void Start()
    {
        NeededItemsDict = new Dictionary<KitchenItemObject, int>();
        foreach(KitchenItemAmount item in neededItems)
        {
            NeededItemsDict.Add(item.item, item.amount);
        }

        trigger.TriggerEnterEvent += OnTriggerEnterEvent; 
    }

    private void OnDestroy()
    {
        trigger.TriggerEnterEvent -= OnTriggerEnterEvent;
    }

    private void OnTriggerEnterEvent(Collider other)
    {
        KitchenItemPickup kitchenItem = other.GetComponent<KitchenItemPickup>();

        if (kitchenItem != null)
        {
            AddItem(kitchenItem);
        }
    }

    public void AddItem(KitchenItemPickup item)
    {
        KitchenItemObject obj = item.InventoryObject;

        if (collectedItemsDict.ContainsKey(obj))
        {
            collectedItemsDict[obj]++;
        }
        else
        {
            collectedItemsDict.Add(obj, 1);
        }

        NewItemAddedEvent?.Invoke(obj);

        Destroy(item.gameObject);

        if(AcquiredAllNeededItems())
        {
            AcquiredAllNeededItemsEvent?.Invoke();
            Debug.Log("Acquired all needed items");
        }
    }

    private bool AcquiredAllNeededItems()
    {
        foreach(KeyValuePair<KitchenItemObject, int> item in NeededItemsDict)
        {
            if(!collectedItemsDict.ContainsKey(item.Key))
            {
                return false;
            }

            if(collectedItemsDict[item.Key] < item.Value)
            {
                return false;
            }
        }

        return true;
    }
}

[Serializable]
public class KitchenItemAmount
{
    public KitchenItemObject item;
    public int amount;
}
