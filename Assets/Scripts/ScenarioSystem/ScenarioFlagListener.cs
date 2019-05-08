using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioFlagListener : MonoBehaviour
{
    [SerializeField] private int flag;

    private ScenarioFlagsService flagSertice;

    private void OnEnable()
    {
        // subscrive
    }

    private void OnDisable()
    {
        //unsubscrive
    }

    // unityEvent
}
