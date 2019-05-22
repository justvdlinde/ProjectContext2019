using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewerUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private ItemInformationPanel itemUI;
    [SerializeField] private Button stopItemViewingButton;
    [SerializeField] private Button zoomButton;
    [SerializeField] private GameObject openSidebarButton;

    public Action StopViewingItemButtonPressedEvent;

    [SerializeField, HideInInspector] private SidebarUI sidebar;
    [SerializeField, HideInInspector] private InGameUI gameUI;

    private void OnValidate()
    {
        sidebar = GetComponent<SidebarUI>();
        gameUI = FindObjectOfType<InGameUI>();
    }

    private void OnEnable()
    {
        stopItemViewingButton.onClick.AddListener(OnCloseItemViewButtonEvent);
        zoomButton.onClick.AddListener(OnItemZoomButtonPressedEvent);
    }

    private void OnDisable()
    {
        stopItemViewingButton.onClick.RemoveListener(OnCloseItemViewButtonEvent);
        zoomButton.onClick.RemoveListener(OnItemZoomButtonPressedEvent);
    }

    public void ShowItemInfoUI(ItemsData data)
    {
        itemUI.Setup(data);
        root.SetActive(true);
        sidebar.Open();
        openSidebarButton.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    private void OnCloseItemViewButtonEvent()
    {
        StopViewingItemButtonPressedEvent?.Invoke();
        openSidebarButton.SetActive(false);
        sidebar.Close();
        gameUI.gameObject.SetActive(true);
    }

    private void OnItemZoomButtonPressedEvent()
    {
        sidebar.Close();
    }
}
