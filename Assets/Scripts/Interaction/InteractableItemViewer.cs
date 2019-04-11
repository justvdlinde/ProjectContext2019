using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class for viewing <see cref="InteractableItem"/>s
/// </summary>
public class InteractableItemViewer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private InteractionHandler input;

    [Header("Object References")]
    [SerializeField] private Camera viewerCamera;
    [SerializeField] private Transform itemContainer;

    [Header("UI")]
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [Header("Fields")]
    [SerializeField] private Layer viewedItemLayer;
    [SerializeField] private float lerpTime;
    [SerializeField] private float rotateSpeed;

    private bool isViewing;
    private IInteractable interactableItem;
    private TransformData itemOriginalTransformData;
    private int itemOriginalLayer;

    private void Start()
    {
        transform.parent = Camera.main.transform;
        
        uiRoot.gameObject.SetActive(false);
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
        if (interactable is InteractableItem)
        {
            StartViewing(interactable);
        }
    }

    private void StartViewing(IInteractable item)
    {
        input.SetActive(false);

        interactableItem = item;
        item.OnInteractionStart();

        StartCoroutine(LerpItemIntoView(item));

        viewerCamera.gameObject.SetActive(true);
        isViewing = true;
        uiRoot.gameObject.SetActive(true);
    }

    private void View()
    {
        // TODO: make it work for mobile
        if (Input.GetMouseButton(0))
        {
            Vector3 relativeUp = Camera.main.transform.TransformDirection(Vector3.up);
            Vector3 relativeRight = Camera.main.transform.TransformDirection(Vector3.right);

            Vector3 objectRelativeUp = itemContainer.transform.InverseTransformDirection(relativeUp);
            Vector3 objectRelaviveRight = itemContainer.transform.InverseTransformDirection(relativeRight);

            Quaternion rotateBy = Quaternion.AngleAxis(-Input.GetAxis("Mouse X") / itemContainer.transform.localScale.x * rotateSpeed, objectRelativeUp)
                                * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") / itemContainer.transform.localScale.x * rotateSpeed, objectRelaviveRight);

            itemContainer.rotation = itemContainer.transform.rotation * rotateBy;
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
    }

    private IEnumerator LerpItemIntoView(IInteractable item)
    {
        itemOriginalLayer = item.GameObject.layer;
        item.GameObject.layer = viewedItemLayer.LayerIndex;
        itemOriginalTransformData = new TransformData(interactableItem.GameObject.transform);

        float timeRemaining = lerpTime;
        Transform itemTransform = item.GameObject.transform;
        TransformData origin = new TransformData(item.GameObject.transform);

        while (timeRemaining > 0)
        {
            float lerp = (lerpTime - timeRemaining) / lerpTime;
            itemTransform.rotation = Quaternion.Lerp(origin.rotation, itemContainer.rotation, lerp);
            itemTransform.position = Vector3.Lerp(origin.position, itemContainer.position, lerp);

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
