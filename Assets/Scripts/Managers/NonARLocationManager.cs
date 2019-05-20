#if UNITY_EDITOR
using UnityEngine;

public class NonARLocationManager : MonoBehaviour
{
    [SerializeField, ScenePath] private string menuScene;
    [SerializeField] private TrackedImageScene[] trackableScenes;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 10, 150, 50), "menu"))
        {
            StartCoroutine(SceneManagerUtility.LoadScene(menuScene));
        }

        for (int i = 0; i < trackableScenes.Length; i++)
        { 
            if (GUI.Button(new Rect(150 + i * 150, 10, 150, 50), trackableScenes[i].name))
            {
                StartCoroutine(SceneManagerUtility.LoadScene(trackableScenes[i].Scene));
            }
        }
    }
}
#endif