using UnityEditor;
using ServiceLocatorNamespace;
using UnityEngine;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ScenarioFlagAttribute))]
public class ScenarioFlagAttributeDrawer : PropertyDrawer
{
    private string[] flagNames;
    private int[] flagHashes;

    private bool init;
    private int selectedHash;

    private void Init(SerializedProperty property)
    {
        ScenarioFlagsService service = (ScenarioFlagsService)ServiceLocator.Instance.Get<ScenarioFlagsService>();
        List<string> flagNamesList = new List<string>(); 
        List<int> flagHashesList = new List<int>(); 
        flagNamesList.Add("None");
        flagHashesList.Add(ScenarioFlag.None);

        foreach(ScenarioFlagCollection collection in service.FlagsCollection)
        {
            foreach(ScenarioFlag flag in collection.collection)
            {
                flagNamesList.Add(collection.name + " - " + flag.name);
                flagHashesList.Add(flag.Hash);
            }
        }

        flagNames = flagNamesList.ToArray();
        flagHashes = flagHashesList.ToArray();

        init = true;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(!init)
        {
            Init(property);
        }

        EditorGUI.BeginProperty(position, GUIContent.none, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        property.intValue = EditorGUI.IntPopup(position, property.intValue, flagNames, flagHashes);

        EditorGUI.EndProperty();
    }
}