#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class NonARLocationManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private List<TrackedImageObject> trackableObjects;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 10, 150, 50), "menu"))
        {
            Show(menu);
        }

        for (int i = 0; i < trackableObjects.Count; i++)
        { 
            if (GUI.Button(new Rect(150 + i * 150, 10, 150, 50), trackableObjects[i].gameObject.name))
            {
                Show(trackableObjects[i].gameObject);
            }
        }
    }

    private void Show(GameObject g)
    {
        menu.SetActive(menu == g);

        for (int i = 0; i < trackableObjects.Count; i++)
        {
            trackableObjects[i].gameObject.SetActive(trackableObjects[i].gameObject == g);
        }
    }
}
#endif