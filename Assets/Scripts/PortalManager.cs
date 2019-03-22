using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject environment;

    private void OnTriggerEnter(Collider collider) {
        environment.SetActive(true);
    }
}