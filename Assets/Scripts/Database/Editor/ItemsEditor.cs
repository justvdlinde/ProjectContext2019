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
[CustomEditor(typeof(Items))]
public class ItemsEditor : BaseGoogleEditor<Items>
{	    
    public override bool Load()
    {        
        Items targetData = target as Items;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<ItemsData>(targetData.WorksheetName) ?? db.CreateTable<ItemsData>(targetData.WorksheetName);
        
        List<ItemsData> myDataList = new List<ItemsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            ItemsData data = new ItemsData();
            
            data = Cloner.DeepCopy<ItemsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
