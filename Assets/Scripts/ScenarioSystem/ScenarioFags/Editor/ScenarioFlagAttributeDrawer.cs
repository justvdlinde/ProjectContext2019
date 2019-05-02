using UnityEditor;
using ServiceLocatorNamespace;
using UnityEngine;

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

        flagNames = new string[service.flagCollection.collection.Count + 1];
        flagNames[0] = "None";
        for (int i = 1; i < flagNames.Length; i++)
        {
            flagNames[i] = service.flagCollection.collection[i - 1].name + " - " + service.flagCollection.collection[i - 1].description;
        }

        flagHashes = new int[service.flagCollection.collection.Count + 1];
        flagHashes[0] = 0;
        for (int i = 1; i < flagHashes.Length; i++)
        {
            flagHashes[i] = service.flagCollection.collection[i - 1].hash;

        }

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