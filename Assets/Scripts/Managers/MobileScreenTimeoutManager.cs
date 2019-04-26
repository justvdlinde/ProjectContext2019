using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileScreenTimeoutManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
