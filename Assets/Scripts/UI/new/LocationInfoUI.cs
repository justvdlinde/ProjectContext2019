using UnityEngine;

public class LocationInfoUI : MonoBehaviour
{
    [SerializeField] private LocationInformationPanel infoPanel;

    [SerializeField, HideInInspector] private SidebarUI sidebar;

    private void OnValidate()
    {
        sidebar = GetComponent<SidebarUI>();
    }

    public void Setup(LocationsData data)
    {
        gameObject.SetActive(true);
        sidebar.Open();
        infoPanel.Setup(data);
    }
}
