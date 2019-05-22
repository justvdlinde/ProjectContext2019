using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Button languageButtonNL;
    [SerializeField] private Button languageButtonEN;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject languageRoot;
    [SerializeField] private GameObject introRoot;

    private void Start()
    {
        languageRoot.SetActive(true);
        introRoot.SetActive(false);
    }

    private void OnEnable()
    {
        languageButtonNL.onClick.AddListener(OnLanguageNLPressed);
        languageButtonEN.onClick.AddListener(OnLanguageENPressed);
        continueButton.onClick.AddListener(OnContinueButotnClicked);
    }

    private void OnDisable()
    {
        languageButtonNL.onClick.RemoveListener(OnLanguageNLPressed);
        languageButtonEN.onClick.RemoveListener(OnLanguageENPressed);
        continueButton.onClick.RemoveListener(OnContinueButotnClicked);
    }

    private void OnLanguageNLPressed()
    {
        ShowIntro();
    }

    private void OnLanguageENPressed()
    {
        ShowIntro();
    }

    private void ShowIntro()
    {
        languageRoot.SetActive(false);
        introRoot.SetActive(true);
    }

    private void OnContinueButotnClicked()
    {
        Destroy(gameObject);
    }
}
