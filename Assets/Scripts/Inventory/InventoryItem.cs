using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public KitchenItemObject Item { get; private set; }

    public InventoryItem(KitchenItemObject item)
    {
        Item = item;
    }
}
