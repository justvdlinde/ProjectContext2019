using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableItem))]
public class InteractableItemEditorDrawer : Editor
{
    private InteractableItem item => (InteractableItem)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = EditorApplication.isPlaying;
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Interaction Start"))
        {
            item.OnInteractionStart();
        }

        if(GUILayout.Button("Interaction Stop"))
        {
            item.OnInteractionStop();
        }
        GUILayout.EndHorizontal();
        GUI.enabled = true;
    }
}
