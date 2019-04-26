using UnityEditor;
using ServiceLocator;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemIDAttribute))]
public class ItemIDAttributeDrawer : PropertyDrawer
{
    private string[] ItemStrings
    {
        get
        {
            if (itemsStrings == null)
            {
                ItemDatabaseService service = (ItemDatabaseService)ServiceLocator.ServiceLocator.Instance.Get<ItemDatabaseService>();
                itemsStrings = new string[service.Items.Length];

                for (int i = 0; i < itemsStrings.Length; i++)
                {
                    itemsStrings[i] = service.Items[i].ID + " - " + service.Items[i].Name;
                }
            }

            return itemsStrings;
        }
    }
    private string[] itemsStrings;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, GUIContent.none, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        property.intValue = EditorGUI.Popup(position, property.intValue, ItemStrings);
        EditorGUI.EndProperty();
    }
}
