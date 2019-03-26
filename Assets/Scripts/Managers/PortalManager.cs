using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneToLoad;

    private void OnTriggerEnter(Collider collider) {
        Debug.Log(sceneToLoad);
        if(sceneToLoad != null) {
            sceneToLoad.SetActive(true);
        }
    }
}