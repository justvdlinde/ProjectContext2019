using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Items")]
    public static void CreateItemsAssetFile()
    {
        Items asset = CustomAssetUtility.CreateAsset<Items>();
        asset.SheetName = "Context-DB";
        asset.WorksheetName = "Items";
        EditorUtility.SetDirty(asset);        
    }
    
}