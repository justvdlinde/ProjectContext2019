using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public GameObject SessionMenuView { get => sessionMenuView; set => sessionMenuView = value; }
    public GameObject RewardMenuView { get => rewardMenuView; set => rewardMenuView = value; }
    public GameObject MapMenuView { get => mapMenuView; set => mapMenuView = value; }

    [Header("MenuItems")]
    [SerializeField] private GameObject mapMenuView;
    [SerializeField] private GameObject rewardMenuView;
    [SerializeField] private GameObject sessionMenuView;

    protected MenuBehavior menuBehavior;

    protected virtual void Start()
    {
        menuBehavior = GetComponent<MenuBehavior>();
    }

    void OnValidate()
    {
        if (Application.isEditor)
        {
            if (MapMenuView == null) MapMenuView = GameObject.Find("MapMenuView");
            if (RewardMenuView == null) RewardMenuView = GameObject.Find("RewardMenuView");
            if (SessionMenuView == null) SessionMenuView = GameObject.Find("SessionMenuView");
        }
    }
}
