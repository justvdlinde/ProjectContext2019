using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupButton : MonoBehaviour
{
    public class Settings
    {
        public string text;
        public Action onClick;
        public bool closePopupOnClick;

        public Settings(string text, Action onClick, bool closePopup = true)
        {
            this.text = text;
            this.onClick = onClick;
            this.closePopupOnClick = closePopup;
        }
    }

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI content;

    private Popup popup;
    private Action clickEvent;
    private bool closeOnClick;

    public void Setup(Popup popup, string text, Action clickEvent, bool closeOnClick)
    {
        this.popup = popup;
        this.clickEvent = clickEvent;
        this.closeOnClick = closeOnClick;

        content.text = text;
        button.onClick.AddListener(OnClickEvent);
    }

    public void Setup(Popup popup, Settings buttonSettings)
    {
        Setup(popup, buttonSettings.text, buttonSettings.onClick, buttonSettings.closePopupOnClick);
    }

    private void OnClickEvent()
    {
        clickEvent?.Invoke();

        if(closeOnClick)
        {
            popup.Close();
        }
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClickEvent);
    }
}
