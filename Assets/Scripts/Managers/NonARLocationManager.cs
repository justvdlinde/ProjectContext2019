#if UNITY_EDITOR
using UnityEngine;

public class NonARLocationManager : MonoBehaviour
{
    [SerializeField] private TrackedImageScene[] trackableScenes;

    private void OnGUI()
    {
        for (int i = 0; i < trackableScenes.Length; i++)
        { 
            if (GUI.Button(new Rect(i * 150, 10, 150, 50), trackableScenes[i].name))
            {
                StartCoroutine(SceneManagerUtility.LoadScene(trackableScenes[i].Scene));
            }
        }
    }
}
#endif