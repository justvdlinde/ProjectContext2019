using System;
using UnityEngine;

public class SceneBoundaryTrigger : MonoBehaviour
{
    public static Action<SceneBoundaryTrigger> BoundaryExitEvent;
    public static Action<SceneBoundaryTrigger> BoundaryEnterEvent;

    [SerializeField] private bool disableMeshAtStart = true;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = !disableMeshAtStart;
    }

    public void OnTriggerExit(Collider other)
    {
        BoundaryExitEvent?.Invoke(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        BoundaryEnterEvent?.Invoke(this);
    }
}
