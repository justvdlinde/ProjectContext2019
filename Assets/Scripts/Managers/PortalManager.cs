using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneToLoad;
    [SerializeField] private GameObject portal;

    private void Start() {
        if (sceneToLoad.activeInHierarchy || portal.activeInHierarchy) {
            sceneToLoad.SetActive(false);
            portal.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if(sceneToLoad != null) {
            sceneToLoad.SetActive(true);
        }
    }
}