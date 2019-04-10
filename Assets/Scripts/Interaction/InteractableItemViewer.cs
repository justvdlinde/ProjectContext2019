using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for viewing <see cref="InteractableItem"/>s
/// </summary>
public class InteractableItemViewer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScreenInput input;

    [Header("Object References")]
    [SerializeField] private Camera viewerCamera;
    [SerializeField] private Transform viewedItemParent;
    [SerializeField] private GameObject uiRoot;
    [SerializeField] private Button closeButton;

    [Header("Fields")]
    [SerializeField] private Layer viewedItemLayer;
    [SerializeField] private float lerpTime;

    private bool isViewing;
    private IInteractable interactableItem;
    private TransformData itemOriginalTransformData;
    private int itemOriginalLayer;

    private void Start()
    {
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
        if(isViewing)
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
        if(interactable is InteractableItem)
        {
            StartViewing(interactable);
        }
    }

    private void StartViewing(IInteractable interactableItem)
    {
        input.SetActive(false);

        this.interactableItem = interactableItem;
        interactableItem.OnInteractionStart();

        StartCoroutine(LerpItemIntoView(interactableItem));

        viewerCamera.gameObject.SetActive(true);
        isViewing = true;
        uiRoot.gameObject.SetActive(true);
    }

    private void View()
    {
       // object roteren
    }

    private void StopViewing()
    {
        StartCoroutine(LerpItemBackToOrigin(interactableItem));

        interactableItem.OnInteractionStop();

        interactableItem = null;
        isViewing = false;
        uiRoot.gameObject.SetActive(false);

        input.SetActive(true);
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
            itemTransform.rotation = Quaternion.Lerp(origin.rotation, viewedItemParent.rotation, lerp);
            itemTransform.position = Vector3.Lerp(origin.position, viewedItemParent.position, lerp);

            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        itemTransform.SetParent(viewedItemParent);
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
    }
}
