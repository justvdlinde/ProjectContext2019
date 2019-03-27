using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField] private Text frameRateValue;
    private int fps;

    private void Update() {
        fps = (int)(1f / Time.unscaledDeltaTime);
        frameRateValue.text = fps.ToString();
    }
}
