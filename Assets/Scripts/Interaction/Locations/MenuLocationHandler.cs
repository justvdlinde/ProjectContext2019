using ServiceLocatorNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuLocationHandler : MonoBehaviour
{
    [SerializeField] private LocationInfoUI locationUI;
    [SerializeField] private new Camera camera;

    private LocationDatabaseService locationsService;
    private RaycastHit hit;
    private Ray ray;
    private Location selectedLocation;

    private void Start()
    {
        locationsService = ServiceLocator.Instance.Get<LocationDatabaseService>() as LocationDatabaseService;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            IInteractable obj = hit.transform.gameObject.GetInterface<IInteractable>();
            Location location = null;

            if (obj != null) { location = obj as Location; }
            else { return; }

            if (location != null)
            {
                OnInteractableObjectHit(location);
            }
        }
    }

    private void OnInteractableObjectHit(Location location)
    {
        locationUI.Setup(locationsService.GetLocationData(location.ID));

        if(selectedLocation != null)
        {
            selectedLocation.OnInteractionStop();
        }

        selectedLocation = location;
        selectedLocation.OnInteractionStart();
    }
}
