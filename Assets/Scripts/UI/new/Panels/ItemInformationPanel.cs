using TMPro;
using UnityEngine;

public class ItemInformationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    public void Setup(ItemsData data)
    {
        title.text = data.Name;
        description.text = data.Description;
    }
}
