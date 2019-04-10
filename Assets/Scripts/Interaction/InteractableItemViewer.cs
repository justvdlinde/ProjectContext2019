using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for viewing <see cref="InteractableItem"/>s
/// </summary>
public class InteractableItemViewer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScreenInput input;

    [Header("Item vars")]
    [SerializeField] private Layer viewedItemLayer;
    [SerializeField] private Camera viewerCamera;
    [SerializeField] private Transform viewedItemParent;
    [SerializeField] private float lerpTime;

    private bool isViewing;
    private IInteractable interactableItem;
    private TransformData itemOriginalTransformData;
    private int itemOriginalLayer;

    private void OnEnable()
    {
        input.InteractedWithObjectEvent += OnInteracedWithObjectEvent;
    }

    private void OnDisable()
    {
        input.InteractedWithObjectEvent -= OnInteracedWithObjectEvent;
    }

    private void Update()
    {
        if(isViewing)
        {
            View();
        }
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
        this.interactableItem = interactableItem;
        interactableItem.OnInteractionStart();

        StartCoroutine(LerpItemIntoView(interactableItem));

        viewerCamera.gameObject.SetActive(true);
        isViewing = true;
    }

    private void View()
    {
       // object roteren

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopViewing();
        }
    }

    private void StopViewing()
    {
        StartCoroutine(LerpItemBackToOrigin(interactableItem));

        interactableItem.OnInteractionStop();

        interactableItem = null;
        isViewing = false;
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
            itemTransform.rotation = Quaternion.Lerp(origin.rotation, viewedItemParent.rotation, (lerpTime - timeRemaining) / lerpTime);
            itemTransform.position = Vector3.Lerp(origin.position, viewedItemParent.position, (lerpTime - timeRemaining) / lerpTime);
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
            itemTransform.rotation = Quaternion.Lerp(start.rotation, itemOriginalTransformData.rotation, (lerpTime - timeRemaining) / lerpTime);
            itemTransform.position = Vector3.Lerp(start.position, itemOriginalTransformData.position, (lerpTime - timeRemaining) / lerpTime);
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        itemTransform.SetFromData(itemOriginalTransformData);
        itemTransform.gameObject.layer = itemOriginalLayer;
        viewerCamera.gameObject.SetActive(false);
    }
}
