using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Settings")]
    public static void CreateSettingsAssetFile()
    {
        Settings asset = CustomAssetUtility.CreateAsset<Settings>();
        asset.SheetName = "Context-DB";
        asset.WorksheetName = "Settings";
        EditorUtility.SetDirty(asset);        
    }
    
}