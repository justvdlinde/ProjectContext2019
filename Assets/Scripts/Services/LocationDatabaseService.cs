using ServiceLocatorNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationDatabaseService : IService
{
    public LocationsData[] Locations => locationDatabase.dataArray;
    private Locations locationDatabase;

    private const string DATABASE_FILE_PATH = "Locations";

    public LocationDatabaseService()
    {
        locationDatabase = Resources.Load<Locations>(DATABASE_FILE_PATH);
    }

    public LocationsData GetItemData(int locationID)
    {
        return locationDatabase.dataArray[locationID];
    }
}
