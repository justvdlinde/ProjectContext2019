using GoogleARCore;
using ServiceLocatorNamespace;
using UnityEngine;

public class TrackedImageObject : MonoBehaviour
{
    public AugmentedImage Image { get; private set; }
    public bool IsBeingTracked { get; private set; }

    private ARManagerService arManager;

    private void Start()
    {
        Hide();
    }

    private void OnEnable()
    {
        if (!arManager)
        {
            arManager = ServiceLocator.Instance.Get<ARManagerService>() as ARManagerService;
        }
        arManager.NewImageTrackedEvent += OnNewImageTrackedEvent;
    }

    private void OnDisable()
    {
        arManager.NewImageTrackedEvent -= OnNewImageTrackedEvent;
    }

    public void Update()
    {
        if (Image != null)        
        {
            if (Image.TrackingState == TrackingState.Tracking)
            {
                transform.localPosition = Image.CenterPose.position;
            }
            else
            {
                Hide();
            }
        }
    }

    public void Show(AugmentedImage image)
    {
        Image = image;
        gameObject.SetActive(true);
        enabled = true;
        IsBeingTracked = true;
    }

    public void Hide()
    {
        Image = null;
        gameObject.SetActive(false);
        enabled = false;
        IsBeingTracked = false;
    }

    private void OnNewImageTrackedEvent(TrackedImageObject o)
    {
        if(o.Image != Image)
        {
            Hide();
        }
    }
}
