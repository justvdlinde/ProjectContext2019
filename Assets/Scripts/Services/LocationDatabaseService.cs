using ServiceLocatorNamespace;
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

    public LocationsData GetLocationData(int locationID)
    {
        return locationDatabase.dataArray[locationID];
    }
}
