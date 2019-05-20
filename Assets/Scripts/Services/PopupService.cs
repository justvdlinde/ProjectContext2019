using UnityEngine;
using ServiceLocatorNamespace;

public class PopupService : IService
{
    private const string POPUP_FILE_PATH = "Popup";

    public Popup InstantiatePopup(string header, string content, PopupButton.Settings button, params PopupButton.Settings[] extraButtons)
    {
        Popup popup = Object.Instantiate(Resources.Load<Popup>(POPUP_FILE_PATH));
        popup.Setup(header, content, button, extraButtons);
        return popup;
    }
}
