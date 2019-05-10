using ServiceLocatorNamespace;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocationIDAttribute))]
public class LocationIDAttributeDrawer : PropertyDrawer
{
    private string[] LocationStrings {
        get {
            if (locationStrings == null)
            {
                LocationDatabaseService service = (LocationDatabaseService)ServiceLocator.Instance.Get<LocationDatabaseService>();
                locationStrings = new string[service.Locations.Length];

                for (int i = 0; i < locationStrings.Length; i++)
                {
                    locationStrings[i] = service.Locations[i].ID + " - " + service.Locations[i].Name;
                }
            }

            return locationStrings;
        }
    }
    private string[] locationStrings;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, GUIContent.none, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        property.intValue = EditorGUI.Popup(position, property.intValue, LocationStrings);
        EditorGUI.EndProperty();
    }
}
