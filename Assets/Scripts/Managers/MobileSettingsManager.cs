using UnityEngine;

public class MobileSettingsManager : MonoBehaviour
{
    [SerializeField] private ScreenOrientation orientation = ScreenOrientation.Landscape;

    private void Start()
    {
        DontDestroyOnLoad(this);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = orientation;
    }
}
