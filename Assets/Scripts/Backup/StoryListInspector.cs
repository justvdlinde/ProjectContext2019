using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(StoryList))]
public class StoryListInspector : Editor
{
    private ReorderableList reorderableList;
    private SerializedProperty poiProperty;

    private StoryList storyList {
        get {
            return target as StoryList;
        }
    }

    private void OnEnable()
    {
        reorderableList = new ReorderableList(storyList.list, typeof(StoryItem), true, true, true, true);

        reorderableList.drawHeaderCallback += DrawHeader;
        reorderableList.drawElementCallback += DrawElement;

        reorderableList.onAddCallback += AddItem;
        reorderableList.onRemoveCallback += RemoveItem;

        poiProperty = serializedObject.FindProperty("storyValue");
    }

    private void OnDisable()
    {
        reorderableList.drawHeaderCallback -= DrawHeader;
        reorderableList.drawElementCallback -= DrawElement;

        reorderableList.onAddCallback -= AddItem;
        reorderableList.onRemoveCallback -= RemoveItem;
    }

    /// <param name="rect"></param>
    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "Chronological story order");
    }

    /// <param name="rect"></param>
    /// <param name="index"></param>
    /// <param name="active"></param>
    /// <param name="focused"></param>
    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        StoryItem item = storyList.list[index];
        EditorGUI.BeginChangeCheck();

        item.storyValue = (POIView)EditorGUI.ObjectField(new Rect(rect.x + 18, rect.y, rect.width - 18, rect.height), item.storyValue, typeof(POIView), true);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void AddItem(ReorderableList list)
    {
        storyList.list.Add(new StoryItem());

        EditorUtility.SetDirty(target);
    }

    private void RemoveItem(ReorderableList list)
    {
        storyList.list.RemoveAt(list.index);

        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        reorderableList.DoLayoutList();
    }
}
