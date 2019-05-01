using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;

[CustomEditor(typeof(ScenarioFlagCollection))]
public class ScenarioFlagCollectionEditor : Editor
{
    private ScenarioFlagCollection Flags { get { return target as ScenarioFlagCollection; } }

    private ReorderableList reorderableList;
    private SerializedProperty property;

    private bool showDetails;
    private string newConditionName = "New Condition";

    private void OnEnable()
    {
        reorderableList = new ReorderableList(Flags.collection, typeof(ScenarioFlag), true, true, true, true);

        reorderableList.drawHeaderCallback += DrawHeader;
        reorderableList.drawElementCallback += DrawElement;
        reorderableList.onAddCallback += AddItem;
        reorderableList.onRemoveCallback += RemoveItem;

        property = serializedObject.FindProperty("collection");
    }

    private void OnDisable()
    {
        reorderableList.drawHeaderCallback -= DrawHeader;
        reorderableList.drawElementCallback -= DrawElement;
        reorderableList.onAddCallback -= AddItem;
        reorderableList.onRemoveCallback -= RemoveItem;
    }

    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "All Conditions");

        showDetails = GUI.Toggle(new Rect(rect.x + rect.width - 70, rect.y, 70, rect.height), showDetails, "Details");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
            
        reorderableList.DoLayoutList();
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        float labelHeight = 15;
        float labelMargin = 5;
        float totalHeight = 0;

        ScenarioFlag item = Flags.collection[index];
        EditorGUI.BeginChangeCheck();

        item.isChecked = EditorGUI.Toggle(new Rect(rect.x, rect.y, 20, labelHeight), item.isChecked);
        item.name = EditorGUI.TextField(new Rect(rect.x + 25, rect.y, rect.width - 25, labelHeight), item.name);
        totalHeight += labelHeight + labelMargin;

        if (showDetails)
        {
            labelHeight = 20 + labelMargin;
            item.description = EditorGUI.TextArea(new Rect(rect.x, rect.y + totalHeight, rect.width, labelHeight), item.description);
            totalHeight += labelHeight;

            EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeight, rect.width, labelHeight), "Hash: " + item.hash.ToString());
            totalHeight += labelHeight;
        }

        reorderableList.elementHeight = totalHeight + labelMargin;

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void AddItem(ReorderableList list)
    {
        ScenarioFlag flag = CreateNewCondition() as ScenarioFlag;

        flag.name = newConditionName;
        flag.hash = Animator.StringToHash(flag.name);
        flag.description = "No description";

        //TODO: if hash already exists in collection, remove it from collection

        Flags.collection.Add(flag);
        EditorUtility.SetDirty(target);
    }

    public ScriptableObject CreateNewCondition()
    {
        ScriptableObject instance = ScriptableObject.CreateInstance(typeof(ScenarioFlag));
        instance.hideFlags = HideFlags.HideInHierarchy;
        instance.name = newConditionName;

        string assetFilePath = AssetDatabase.GetAssetPath(Flags);
        AssetDatabase.AddObjectToAsset(instance, assetFilePath);

        return instance;
    }

    private void RemoveItem(ReorderableList list)
    {
        Flags.collection.RemoveAt(list.index);

        EditorUtility.SetDirty(target);
    }
}
