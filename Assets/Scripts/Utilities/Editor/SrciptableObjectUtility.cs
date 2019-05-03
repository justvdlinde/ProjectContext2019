using System;
using UnityEditor;
using UnityEngine;

public static class SrciptableObjectUtility
{
    public static ScriptableObject CreateObjectInstance(Type type, string assetFilePath)
    {
        ScriptableObject instance = ScriptableObject.CreateInstance(type);
        instance.hideFlags = HideFlags.HideInHierarchy;

         AssetDatabase.AddObjectToAsset(instance, assetFilePath);
        return instance;
    }
}
