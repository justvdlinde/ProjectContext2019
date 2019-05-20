using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScanUI scanUI;

    [Header("UI Fields")]
    [SerializeField] private TogglePanelPair info;
    [SerializeField] private TogglePanelPair options;
    [SerializeField] private Button scanButton;

    [SerializeField, HideInInspector] private SidebarUI sidebarUI;

    private ARManagerService arManager;

    private void OnValidate()
    {
        sidebarUI = GetComponent<SidebarUI>();
        scanUI = FindObjectOfType<ScanUI>();
    }

    private void Start()
    {
        arManager = ServiceLocatorNamespace.ServiceLocator.Instance.Get<ARManagerService>() as ARManagerService;
    }

    private void OnEnable()
    {
        info.OnEnable();
        options.OnEnable();

        scanButton.onClick.AddListener(OnScanButtonClickedEvent);
        sidebarUI.CloseEvent += OnSidebarCloseEvent;
        sidebarUI.OpenEvent += OnSidebarOpenEvent;
    }

    private void OnDisable()
    {
        info.OnDisable();
        options.OnDisable();

        scanButton.onClick.RemoveListener(OnScanButtonClickedEvent);
        sidebarUI.CloseEvent -= OnSidebarCloseEvent;
        sidebarUI.OpenEvent -= OnSidebarOpenEvent;
    }

    private void OnScanButtonClickedEvent()
    {
        scanUI.ShowScanOverlay(true);
        arManager.EnableAR(true);
        gameObject.SetActive(false);
    }

    private void OnSidebarOpenEvent()
    {
        scanButton.gameObject.SetActive(false);
    }

    private void OnSidebarCloseEvent()
    {
        scanButton.gameObject.SetActive(true);
    }
}
