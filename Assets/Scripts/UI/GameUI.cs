using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Game UI")]
    [SerializeField] private GameObject gameUIRoot;
    [SerializeField] private Button menuButton;
    [SerializeField] private Toggle pauseToggle;

    [Header("Menu UI")]
    [SerializeField] private GameObject pauseUIRoot;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        pauseUIRoot.SetActive(false);
        pauseToggle.isOn = false;
    }

    private void OnEnable()
    {
        menuButton.onClick.AddListener(OnMenuButtonPressed);
        pauseToggle.onValueChanged.AddListener(OnPauseTogglePressed);

        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        menuButton.onClick.RemoveListener(OnMenuButtonPressed);
        pauseToggle.onValueChanged.RemoveListener(OnPauseTogglePressed);

        resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnMenuButtonPressed()
    {
        IsPaused = true;
        pauseUIRoot.SetActive(IsPaused);
        gameUIRoot.SetActive(!IsPaused);

        GameTimeManager.PauseGame();
    }

    private void OnResumeButtonClicked()
    {
        pauseUIRoot.SetActive(false);
        gameUIRoot.SetActive(true);

        GameTimeManager.ResumeGame();
    }

    private void OnQuitButtonClicked()
    {
        IsPaused = false;
        pauseUIRoot.SetActive(false);
        GameTimeManager.ResumeGame();

        //TODO: quit game, show main menu UI
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
}
