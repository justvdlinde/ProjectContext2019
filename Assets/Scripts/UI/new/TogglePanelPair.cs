using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TogglePanelPair
{
    public Toggle toggle;
    public GameObject panel;

    public void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnSelectEvent);
    }

    public void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnSelectEvent);
    }

    private void OnSelectEvent(bool select)
    {
        panel.SetActive(select);
    }
}
