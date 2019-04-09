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
    [SerializeField] private GameObject viewerCamera;
    [SerializeField] private Transform viewedItemParent;
    [SerializeField] private float lerpSpeed;

    private bool isViewing;
    private IInteractable interactableItem;
    private TransformData itemOriginalTransformData;
    private LayerMask itemOriginalLayer;

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

        itemOriginalLayer = interactableItem.GameObject.layer;
        interactableItem.GameObject.layer = viewedItemLayer.LayerIndex;

        itemOriginalTransformData = new TransformData(interactableItem.GameObject.transform);
        interactableItem.GameObject.transform.SetParent(viewedItemParent);
        interactableItem.GameObject.transform.localPosition = Vector3.zero;

        viewerCamera.SetActive(true);
        isViewing = true;
    }

    private void View()
    {
       // object roteren
    }

    private void StopViewing()
    {
        interactableItem.OnInteractionStop();

        interactableItem.GameObject.transform.SetFromData(itemOriginalTransformData);
        interactableItem.GameObject.layer = itemOriginalLayer;

        viewerCamera.SetActive(false);

        interactableItem = null;
        isViewing = false;
    }

    //private IEnumerator LerpIntoView(Transform item)
    //{
    //    Timer timer = new Timer(timeInSeconds);
    //    float fillAtStart = ProgressImage.fillAmount;
    //    float difference = targetFillAmount - ProgressImage.fillAmount;

    //    while (!timer.expired)
    //    {
    //        progressImage.fillAmount = fillAtStart + difference * timer.progress / timer.duration;
    //        yield return null;
    //    }

    //    item.position = ;
    //}
}
