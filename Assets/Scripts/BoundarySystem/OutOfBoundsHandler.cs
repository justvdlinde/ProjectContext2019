using UnityEngine;
using UnityEngine.UI;

public class OutOfBoundsHandler : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadeRoot;

    private void Start()
    {
        fadeRoot.alpha = 0;
    }

    private void OnEnable()
    {
        SceneBoundaryTrigger.BoundaryExitEvent += OnBoundaryExitEvent;
        SceneBoundaryTrigger.BoundaryEnterEvent += OnBoundaryEnterEvent;
    }

    private void OnDisable()
    {
        SceneBoundaryTrigger.BoundaryExitEvent -= OnBoundaryExitEvent;
        SceneBoundaryTrigger.BoundaryEnterEvent -= OnBoundaryEnterEvent;
    }

    private void OnBoundaryExitEvent(SceneBoundaryTrigger trigger)
    {
        fadeRoot.alpha = 1;
    }

    private void OnBoundaryEnterEvent(SceneBoundaryTrigger trigger)
    {
        fadeRoot.alpha = 0;
    }
}
