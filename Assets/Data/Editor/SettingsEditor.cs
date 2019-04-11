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
[CustomEditor(typeof(Settings))]
public class SettingsEditor : BaseGoogleEditor<Settings>
{	    
    public override bool Load()
    {        
        Settings targetData = target as Settings;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<SettingsData>(targetData.WorksheetName) ?? db.CreateTable<SettingsData>(targetData.WorksheetName);
        
        List<SettingsData> myDataList = new List<SettingsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            SettingsData data = new SettingsData();
            
            data = Cloner.DeepCopy<SettingsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
