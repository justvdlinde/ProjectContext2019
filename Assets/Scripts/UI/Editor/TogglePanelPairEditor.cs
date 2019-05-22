using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TogglePanelPair))]
public class TogglePanelPairEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect rect = new Rect(position.x, position.y, position.width / 2, position.height);
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("toggle"), GUIContent.none);

        rect = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("panel"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
