using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Scenario))]
public class ScenarioEditor : Editor
{
    Scenario scenario;

    private void OnEnable()
    {
        scenario = target as Scenario;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //if (GUILayout.Button("Update Can Start Value"))
        //{
        //    scenario.UpdateCanStartValue();
        //}

        //if (GUILayout.Button("Start Scenario"))
        //{
        //    scenario.StartScenario();
        //}

        //if (GUILayout.Button("Complete Scenario"))
        //{
        //    scenario.EndScenario();
        //}
    }
}
