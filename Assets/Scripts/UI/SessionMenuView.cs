using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionMenuView : MenuBehavior
{
    [Header("Session")]
    [SerializeField] private GoogleARCore.ARCoreSession session;
    [SerializeField] private GoogleARCore.Examples.AugmentedImage.AugmentedImageExampleController sessionController;

    [Header("Buttons")]
    [SerializeField] private Button endGameButton;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        endGameButton.onClick.AddListener(EndGame);
    }

    private void OnDisable()
    {
        endGameButton.onClick.RemoveListener(EndGame);
    }

    private void EndGame()
    {
        session.enabled = false;
        sessionController.enabled = false;
        gameObject.SetActive(false);
        menuBehavior.RewardMenuView.SetActive(true);
    }
}
