using System;
using UnityEngine;
using UnityEngine.UI;

public class SidebarUI : MonoBehaviour
{
    [SerializeField] private Button openSidebarButton;
    [SerializeField] private Button closeSidebarButton;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform sidebar;
    [SerializeField] private ToggleGroup toggleGroup;

    public const string ANIMATOR_OPEN_TRIGGER = "Open";
    public const string ANIMATOR_CLOSE_TRIGGER = "Close";

    public Action OpenEvent;
    public Action CloseEvent;

    protected virtual void Awake()
    {
        animator.enabled = false;
        sidebar.gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        openSidebarButton.onClick.AddListener(OnOpenButtonClickedEvent);
        closeSidebarButton.onClick.AddListener(OnCloseButtonClickedEvent);
    }

    protected virtual void OnDisable()
    {
        openSidebarButton.onClick.RemoveListener(OnOpenButtonClickedEvent);
        closeSidebarButton.onClick.RemoveListener(OnCloseButtonClickedEvent);
    }

    private void OnCloseButtonClickedEvent()
    {
        Close();
    }

    private void OnOpenButtonClickedEvent()
    {
        Open();
    }

    public void Close()
    {
        animator.enabled = true;
        animator.SetTrigger(ANIMATOR_CLOSE_TRIGGER);

        CloseEvent?.Invoke();
    }

    public void Open()
    {
        animator.enabled = true;
        animator.SetTrigger(ANIMATOR_OPEN_TRIGGER);

        OpenEvent?.Invoke();
    }
}
