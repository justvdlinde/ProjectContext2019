using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Game UI")]
    [SerializeField] private GameObject gameUIRoot;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button pauseButton;

    [Header("Menu UI")]
    [SerializeField] private GameObject pauseUIRoot;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject mainMenuButton;

    public bool IsPaused { get; private set; }

    private void Start()
    {
        pauseUIRoot.SetActive(false);
    }

    private void OnEnable()
    {
        menuButton.onClick.AddListener(OnMenuButtonPressed);
        menuButton.onClick.AddListener(OnPauseButtonPressed);
    }

    private void OnDisable()
    {
        menuButton.onClick.RemoveListener(OnMenuButtonPressed);
        menuButton.onClick.RemoveListener(OnPauseButtonPressed);
    }

    private void OnMenuButtonPressed()
    {
        IsPaused = !IsPaused;
        pauseUIRoot.SetActive(!IsPaused);
        gameUIRoot.SetActive(IsPaused);

        if (IsPaused)
        {
            GameTimeManager.ResumeGame();
        }
        else
        { 
            GameTimeManager.PauseGame();
        }
    }

    private void OnPauseButtonPressed()
    {
        if (GameTimeManager.CurrentTimeState == GameTimeManager.TimeState.Normal)
        {
            GameTimeManager.PauseGame();
        }
        else
        {
            GameTimeManager.ResumeGame();
        }
    }
}
