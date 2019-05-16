using UnityEngine;
using UnityEngine.UI;

public class OutOfBoundsHandler : MonoBehaviour
{
    [SerializeField] private MenuScreen menu;

    private void OnEnable()
    {
        SceneBoundaryTrigger.BoundaryExitEvent += OnBoundaryExitEvent;
    }

    private void OnDisable()
    {
        SceneBoundaryTrigger.BoundaryExitEvent -= OnBoundaryExitEvent;
    }

    private void OnBoundaryExitEvent(SceneBoundaryTrigger trigger)
    {
        menu.ShowMenu(true);
    }
}
