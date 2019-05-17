using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationPopup : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image portalImage;

    // var locationService

    private void OnEnable()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(Hide);
    }

    public void Show(int locationID)
    {
        gameObject.SetActive(true);

        // TODO: 
        // fill title and description via locationService
        // fill corresponding portalImage
        // fill items found amount + story completed check
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
