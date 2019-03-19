using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private ScreenInput input;

    [SerializeField] private KitchenItemAmount[] neededItems;
    public KitchenItemAmount[] NeededItems => neededItems;

    public Dictionary<KitchenItemObject, int> collectedItemsAmountPair = new Dictionary<KitchenItemObject, int>();

    private void OnEnable()
    {
        input.InteractedWithObjectEvent += OnInteractedWithObjectEvent;
    }

    private void OnDisable()
    {
        input.InteractedWithObjectEvent -= OnInteractedWithObjectEvent;
    }

    private void OnInteractedWithObjectEvent(IInteractable interactable)
    {
        if(interactable is KitchenItemPickup)
        {
            AddItem(interactable as KitchenItemPickup);
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

        foreach (KeyValuePair<KitchenItemObject, int> kvp in collectedItemsAmountPair)
        {
            Debug.Log(kvp.Key.item + " amount " + kvp.Value);
        }
    }
}

[System.Serializable]
public class KitchenItemAmount
{
    public KitchenItemObject item;
    public int amount;
}
