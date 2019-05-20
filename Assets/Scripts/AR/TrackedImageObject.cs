using GoogleARCore;
using ServiceLocatorNamespace;
using UnityEngine;

public class TrackedImageObject : MonoBehaviour
{
    public AugmentedImage Image { get; private set; }
    public bool IsBeingTracked { get; private set; }

    private void Start()
    {
        Hide();
    }

    public virtual void Update()
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

    public virtual void Show(AugmentedImage image)
    {
        Image = image;
        gameObject.SetActive(true);
        enabled = true;
        IsBeingTracked = true;
    }

    public virtual void Hide()
    {
        Image = null;
        gameObject.SetActive(false);
        enabled = false;
        IsBeingTracked = false;
    }
}
