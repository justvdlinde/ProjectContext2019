using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ServiceLocatorNamespace;

public class LocationInformationPanel : MonoBehaviour
{
    [SerializeField] private Image markerImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI information;
    [SerializeField] private TextMeshProUGUI itemsFound;
    [SerializeField] private Image storyCompletedCheck;

    public void Setup(LocationsData data)
    {
        title.text = data.Name;
        information.text = data.Description;

        // TODO: rest van UI
    }
}
