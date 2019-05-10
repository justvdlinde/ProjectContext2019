using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationViewer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [Header("Locations")]
    [SerializeField] private Button location1;
    [SerializeField] private Button location2;
    [SerializeField] private Button location3;

    private bool isViewing;

    private LocationDatabaseService itemDatabase;

    private void Start()
    {
        uiRoot.gameObject.SetActive(false);
        itemDatabase = (LocationDatabaseService)ServiceLocatorNamespace.ServiceLocator.Instance.Get<LocationDatabaseService>();

        location1.onClick.AddListener(() => OnInteracedWithObjectEvent(location1.gameObject.GetComponent<Location>()));
        location2.onClick.AddListener(() => OnInteracedWithObjectEvent(location2.gameObject.GetComponent<Location>()));
        location3.onClick.AddListener(() => OnInteracedWithObjectEvent(location3.gameObject.GetComponent<Location>()));
        closeButton.onClick.AddListener(() => StopViewing());
    }

    private void OnInteracedWithObjectEvent(Location location)
    {
        if (isViewing) { return; }
        StartViewing(location);
    }

    private void StartViewing(Location location)
    {
        isViewing = true;
        uiRoot.gameObject.SetActive(true);
        SetUI(itemDatabase.GetItemData(location.ID));
    }

    private void StopViewing()
    {
        isViewing = false;
        uiRoot.gameObject.SetActive(false);
    }

    private void SetUI(LocationsData data)
    {
        itemName.text = data.Name;
        bool hasDescription = data.Description != string.Empty;
        itemDescription.text = data.Description;
    }

}
