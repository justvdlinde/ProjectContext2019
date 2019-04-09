using UnityEngine;

public class MobileOrientationManager : MonoBehaviour
{
    [SerializeField] private ScreenOrientation orientation = ScreenOrientation.Landscape;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Screen.orientation = orientation;
    }
}
