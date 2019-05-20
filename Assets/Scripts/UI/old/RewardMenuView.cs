using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMenuView : MenuBehavior
{
    [Header("Buttons")]
    [SerializeField] private Button returnButton;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        returnButton.onClick.AddListener(() => Return());
    }

    private void OnDisable()
    {
        returnButton.onClick.RemoveListener(() => Return());
    }

    private void Return()
    {
        menuBehavior.RewardMenuView.SetActive(false);
        menuBehavior.MapMenuView.SetActive(true);
    }


}
