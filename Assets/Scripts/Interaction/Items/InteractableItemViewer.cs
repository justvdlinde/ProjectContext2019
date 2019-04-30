using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ServiceLocator;

/// <summary>
/// Class for viewing <see cref="InteractableItem"/>s
/// </summary>
public class InteractableItemViewer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private InteractionHandler input;

    [Header("Object References")]
    [SerializeField] private Camera viewerCamera;
    [SerializeField] private Transform itemContainer;

    [Header("UI")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private GameObject itemDescriptionPopup;
    [SerializeField] private Button itemDescriptionPopupCloseButton;

    [Header("Fields")]
    [SerializeField] private Layer viewedItemLayer;
    [SerializeField] private float lerpTime;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateSpeedMobile;

    private bool isViewing;
    private IInteractable interactableItem;
    private TransformData itemOriginalTransformData;
    private int itemOriginalLayer;

    private ItemDatabaseService itemDatabase;

    private void Start()
    {
        transform.SetParent(Camera.main.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        uiRoot.gameObject.SetActive(false);

        itemDatabase = (ItemDatabaseService)ServiceLocator.ServiceLocator.Instance.Get<ItemDatabaseService>();
    }

    private void OnEnable()
    {
        input.InteractedWithObjectEvent += OnInteracedWithObjectEvent;
        closeButton.onClick.AddListener(OnCloseButtonPressed);
    }

    private void OnDisable()
    {
        input.InteractedWithObjectEvent -= OnInteracedWithObjectEvent;
        closeButton.onClick.RemoveListener(OnCloseButtonPressed);
    }

    private void Update()
    {
        if (isViewing)
        {
            View();
        }
    }

    private void OnCloseButtonPressed()
    {
        StopViewing();
    }

    private void OnInteracedWithObjectEvent(IInteractable interactable)
    {
        if (isViewing) { return; }

        if (interactable is InteractableItem)
        {
            StartViewing(interactable as InteractableItem);
        }
    }

    private void StartViewing(InteractableItem item)
    {
        input.SetActive(false);

        isViewing = true;
        interactableItem = item;
        item.OnInteractionStart();

        StartCoroutine(LerpItemIntoView(item));

        viewerCamera.gameObject.SetActive(true);
        uiRoot.gameObject.SetActive(true);

        SetUI(itemDatabase.GetItemData(item.ID));
    }

    private void SetUI(ItemsData data)
    {
        itemName.text = data.Name;

        bool hasDescription = data.Description != string.Empty;
        itemDescriptionPopup.SetActive(hasDescription);
        if (hasDescription)
        {
            itemDescription.text = data.Description;
            itemDescriptionPopupCloseButton.onClick.AddListener(OnClosePopupButtonPressed);
        }
    }

    private void OnClosePopupButtonPressed()
    {
        itemDescriptionPopup.SetActive(false);
        itemDescriptionPopupCloseButton.onClick.RemoveListener(OnClosePopupButtonPressed);
    }

    private void View()
    {
        if (Input.touchCount > 0)
        { 
            Debug.Log(Input.touches[0].deltaPosition);
        }

        if (Input.GetMouseButton(0))
        {
            float horizontalDelta = 0;
            float verticalDelta = 0;
            float rotateSpeed = this.rotateSpeed;

            // TODO: refactor this to be cleaner
            if (Input.touchCount > 0)
            {
                horizontalDelta = Input.touches[0].deltaPosition.x;
                verticalDelta = Input.touches[0].deltaPosition.y;
                rotateSpeed = rotateSpeedMobile;
            } 
            else
            {
                horizontalDelta = Input.GetAxis("Mouse X");
                verticalDelta = Input.GetAxis("Mouse Y");
            }

            Vector3 relativeUp = Camera.main.transform.TransformDirection(Vector3.up);
            Vector3 relativeRight = Camera.main.transform.TransformDirection(Vector3.right);

            Vector3 objectRelativeUp = itemContainer.transform.InverseTransformDirection(relativeUp);
            Vector3 objectRelaviveRight = itemContainer.transform.InverseTransformDirection(relativeRight);

            Quaternion extraRotation = Quaternion.AngleAxis(-horizontalDelta / itemContainer.transform.localScale.x * rotateSpeed, objectRelativeUp)
                                        * Quaternion.AngleAxis(verticalDelta / itemContainer.transform.localScale.y * rotateSpeed, objectRelaviveRight);

            itemContainer.rotation = itemContainer.transform.rotation * extraRotation;
        }
    }

    private void StopViewing()
    {
        StartCoroutine(LerpItemBackToOrigin(interactableItem));

        interactableItem.OnInteractionStop();
        itemContainer.rotation = Quaternion.identity;

        interactableItem = null;
        isViewing = false;
        uiRoot.gameObject.SetActive(false);
        itemDescriptionPopupCloseButton.onClick.RemoveListener(OnClosePopupButtonPressed);
    }

    private IEnumerator LerpItemIntoView(IInteractable item)
    {
        itemOriginalLayer = item.GameObject.layer;
        item.GameObject.layer = viewedItemLayer.LayerIndex;

        float timeRemaining = lerpTime;
        Transform itemTransform = item.GameObject.transform;
        itemOriginalTransformData = new TransformData(itemTransform);

        while (timeRemaining > 0)
        {
            float lerp = (lerpTime - timeRemaining) / lerpTime;
            itemTransform.rotation = Quaternion.Lerp(itemOriginalTransformData.rotation, itemContainer.rotation, lerp);
            itemTransform.position = Vector3.Lerp(itemOriginalTransformData.position, itemContainer.position, lerp);

            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        itemTransform.SetParent(itemContainer);
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;
    }

    private IEnumerator LerpItemBackToOrigin(IInteractable item)
    {
        float timeRemaining = lerpTime;
        Transform itemTransform = item.GameObject.transform;
        TransformData start = new TransformData(itemTransform);

        while (timeRemaining > 0)
        {
            float lerp = (lerpTime - timeRemaining) / lerpTime;
            itemTransform.rotation = Quaternion.Lerp(start.rotation, itemOriginalTransformData.rotation, lerp);
            itemTransform.position = Vector3.Lerp(start.position, itemOriginalTransformData.position, lerp);

            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        itemTransform.SetFromData(itemOriginalTransformData);
        itemTransform.gameObject.layer = itemOriginalLayer;
        viewerCamera.gameObject.SetActive(false);

        input.SetActive(true);
    }
}
