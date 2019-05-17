using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI locationTitle;
    [SerializeField] private TextMeshProUGUI storyDescription;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(Close);

        FillUI();
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(Close);
    }

    private void FillUI()
    {
        // TODO: 
        // set locationTitle en story description
        // set items found + story completed check
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
