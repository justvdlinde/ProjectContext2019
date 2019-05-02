using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocatorNamespace;

public class ItemDatabaseService : IService
{
    public ItemsData[] Items => itemsDatabase.dataArray;
    private Items itemsDatabase;

    private const string DATABASE_FILE_PATH = "Items";

    public ItemDatabaseService()
    {
        itemsDatabase = Resources.Load<Items>(DATABASE_FILE_PATH);
    }

    public ItemsData GetItemData(int itemID)
    {
        return itemsDatabase.dataArray[itemID];
    }
}
