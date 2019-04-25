using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public static Action<ScenarioEnumFlags> OnScenarioFinished;

    public ScenarioStatus status;
    public ScenarioEnumFlags flag;
    public bool canStart;

    private ScenarioManager scenarioManager;

    private void OnValidate()
    {
        name = "Scenario: " + flag;

        scenarioManager = FindObjectOfType<ScenarioManager>();
    }

    public void UpdateCanStartValue()
    {
        // needs to check for lower than flag
        canStart = scenarioManager.ContainsAllLowerFlags(flag);
    }

    public void StartScenario()
    {
        status = ScenarioStatus.InProgress;
    }

    public void EndScenario()
    {
        status = ScenarioStatus.Completed;
        OnScenarioFinished?.Invoke(flag);
    }
}
