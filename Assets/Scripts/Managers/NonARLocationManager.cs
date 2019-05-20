#if UNITY_EDITOR
using UnityEngine;

public class NonARLocationManager : MonoBehaviour
{
    [SerializeField] private TrackedImageScene[] trackableScenes;

    private SceneManagerService sceneManager;

    private void Start()
    {
        sceneManager = ServiceLocatorNamespace.ServiceLocator.Instance.Get<SceneManagerService>() as SceneManagerService;
    }

    private void OnGUI()
    {
        for (int i = 0; i < trackableScenes.Length; i++)
        { 
            if (GUI.Button(new Rect(i * 150, 10, 150, 50), trackableScenes[i].name))
            {
                sceneManager.LoadScene(trackableScenes[i].Scene);
            }
        }
    }
}
#endif