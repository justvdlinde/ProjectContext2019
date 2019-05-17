using System;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Transform buttonGroup;
    [SerializeField] private PopupButton button;

    public void Setup(string header, string content, PopupButton.Settings buttonSetting, params PopupButton.Settings[] extraButtonsSettings)
    {
        this.header.text = header;
        this.content.text = content;

        for(int i = 0; i < extraButtonsSettings.Length; i++)
        {
            PopupButton newButton = Instantiate(button);
            newButton.Setup(this, extraButtonsSettings[i]);
            newButton.transform.SetParent(buttonGroup, false);
        }

        button.Setup(this, buttonSetting);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}

