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
        string prevName = item.name;
        item.name = EditorGUI.DelayedTextField(new Rect(rect.x + 25, rect.y, rect.width - 25, labelHeight), item.name);
        totalHeight += labelHeight + labelMargin;

        if (showDetails)
        {
            labelHeight = 20 + labelMargin;
            item.description = EditorGUI.TextArea(new Rect(rect.x, rect.y + totalHeight, rect.width, labelHeight), item.description);
            totalHeight += labelHeight;

            EditorGUI.LabelField(new Rect(rect.x, rect.y + totalHeight, rect.width, labelHeight), "Hash: " + item.Hash.ToString());
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

        int hash = GenerateHash(flag);

        if (ContainsHash(hash))
        {
            Debug.LogWarningFormat("Collection already contains a flag with the that hash! name: {0} hash: {1}", newConditionName, hash);
            return;
        }

        flag.name = newConditionName;
        flag.SetHash(hash);
        flag.description = "No description";

        Flags.collection.Add(flag);
        EditorUtility.SetDirty(target);
    }

    private bool ContainsHash(int hash)
    {
        foreach(ScenarioFlag flag in Flags.collection)
        {
            if(flag.Hash == hash)
            {
                return true;
            }
        }

        return false;
    }

    public ScriptableObject CreateNewCondition()
    {
        ScriptableObject instance = ScriptableObject.CreateInstance(typeof(ScenarioFlag));
        instance.hideFlags = HideFlags.None;
        instance.name = newConditionName;

        string assetFilePath = AssetDatabase.GetAssetPath(Flags);
        AssetDatabase.AddObjectToAsset(instance, assetFilePath);
        AssetDatabase.ImportAsset(assetFilePath);

        return instance;
    }

    private void RemoveItem(ReorderableList list)
    {
        ScenarioFlag flag = Flags.collection[list.index];
        Flags.collection.RemoveAt(list.index);

        string assetFilePath = AssetDatabase.GetAssetPath(Flags);
        AssetDatabase.RemoveObjectFromAsset(flag);
        AssetDatabase.ImportAsset(assetFilePath);

        EditorUtility.SetDirty(target);
    }

    private int GenerateHash(ScriptableObject scriptableObject)
    {
        return scriptableObject.GetInstanceID();
    }
}
