using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Locations")]
    public static void CreateLocationsAssetFile()
    {
        Locations asset = CustomAssetUtility.CreateAsset<Locations>();
        asset.SheetName = "Context-DB";
        asset.WorksheetName = "Locations";
        EditorUtility.SetDirty(asset);        
    }
    
}