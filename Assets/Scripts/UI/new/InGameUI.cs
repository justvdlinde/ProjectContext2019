using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField, ScenePath] string menuScene;

    [SerializeField] private GameObject sidebarRoot;
    [SerializeField] private GameObject itemViewerRoot;
    [SerializeField] private Button stopItemViewingButton;
    [SerializeField] private TextMeshProUGUI location;
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Toggle pauseButton;
    [SerializeField] private TogglePanelPair infoPair;
    [SerializeField] private TogglePanelPair settingsPair;

    [SerializeField, HideInInspector] private SidebarUI sidebar;
    [SerializeField, HideInInspector] private ItemInformationPanel itemUI;

    private PopupService popupService;
    private SceneManagerService sceneManager;

    private void OnValidate()
    {
        sidebar = GetComponent<SidebarUI>();
        itemUI = GetComponentInChildren<ItemInformationPanel>();
    }

    private void Start()
    {
        popupService = ServiceLocatorNamespace.ServiceLocator.Instance.Get<PopupService>() as PopupService;
        sceneManager = ServiceLocatorNamespace.ServiceLocator.Instance.Get<SceneManagerService>() as SceneManagerService;
    }

    private void OnEnable()
    {
        homeButton.onClick.AddListener(OnHomeButtonPressedEvent);
        replayButton.onClick.AddListener(OnReplayButtonPressedEvent);
        pauseButton.onValueChanged.AddListener(OnPauseTogglePressed);
        stopItemViewingButton.onClick.AddListener(OnCloseItemViewButtonEvent);

        infoPair.OnEnable();
        settingsPair.OnEnable();

        SetupIntroUI();
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveListener(OnHomeButtonPressedEvent);
        replayButton.onClick.RemoveListener(OnReplayButtonPressedEvent);
        pauseButton.onValueChanged.RemoveListener(OnPauseTogglePressed);
        stopItemViewingButton.onClick.RemoveListener(OnCloseItemViewButtonEvent);

        infoPair.OnDisable();
        settingsPair.OnDisable();
    }

    private void SetupIntroUI()
    {
        // TODO: location + date a.d.h.v current location
    }

    private void OnHomeButtonPressedEvent()
    {
        sidebar.Close();
        sceneManager.LoadScene(menuScene);

        // TODO: return to main scene
    }

    private void OnPauseTogglePressed(bool toggle)
    {
        if (toggle)
        {
            GameTimeManager.PauseGame();
        }
        else
        {
            GameTimeManager.ResumeGame();
        }
    }

    public void ShowItemInfoUI(ItemsData data)
    {
        sidebarRoot.SetActive(false);
        itemViewerRoot.SetActive(true);
        itemUI.Setup(data);
    }

    private void OnCloseItemViewButtonEvent()
    {
        sidebarRoot.SetActive(true);
        itemViewerRoot.SetActive(false);
    }

    private void OnReplayButtonPressedEvent()
    {

        popupService.InstantiatePopup(
            "Error",
            "Deze functie werkt nog niet",
            new PopupButton.Settings("Ok", null)
        );

        // TODO: call scenario reset functionality
        //popupService.InstantiatePopup(
        //    "",
        //    "Weet u zeker dat u het verhaal opnieuw wilt afspelen?",
        //    new PopupButton.Settings("Ja", null),
        //    new PopupButton.Settings("Nee", null)
        //);
    }
}
