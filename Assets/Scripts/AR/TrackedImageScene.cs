using UnityEngine;
using GoogleARCore;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrackedImageScene : TrackedImageObject
{
    [SerializeField, ScenePath] private string scene;
    public string Scene => scene;

    private ARSceneAnchor sceneAnchor;

    public override void Show(AugmentedImage image)
    {
        base.Show(image);
        StartCoroutine(SceneManagerUtility.LoadScene(scene, OnDoneLoadingEvent));
    }

    private void OnDoneLoadingEvent()
    {
        sceneAnchor = FindObjectOfType<ARSceneAnchor>();
    }

    public override void Update()
    {
        if (Image != null)
        {
            if (Image.TrackingState == TrackingState.Tracking)
            {
                sceneAnchor.transform.position = Image.CenterPose.position;
            }
            else
            {
                Hide();
            }
        }
    }
}
