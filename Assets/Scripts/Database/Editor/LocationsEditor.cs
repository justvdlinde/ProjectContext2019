using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Locations))]
public class LocationsEditor : BaseGoogleEditor<Locations>
{	    
    public override bool Load()
    {        
        Locations targetData = target as Locations;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<LocationsData>(targetData.WorksheetName) ?? db.CreateTable<LocationsData>(targetData.WorksheetName);
        
        List<LocationsData> myDataList = new List<LocationsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            LocationsData data = new LocationsData();
            
            data = Cloner.DeepCopy<LocationsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
