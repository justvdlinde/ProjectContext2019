using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScenarioTestItem))]
public class ScenarioItemEditor : Editor
{
    private ScenarioTestItem Item { get { return target as ScenarioTestItem; } }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        GUI.enabled = Item.Status == ScenarioStatus.NotStarted;
        if(GUILayout.Button("Start Scenario"))
        {
            Item.StartScenario();
        }
        GUI.enabled = true;

        GUI.enabled = Item.Status == ScenarioStatus.InProgress;
        if (GUILayout.Button("Complete Scenario"))
        {
            Item.CompleteScenario();
        }
        GUI.enabled = true;
        GUILayout.EndHorizontal();
    }
}
