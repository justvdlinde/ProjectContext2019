using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject environment;
    [SerializeField] private Text debugText;

    private void OnTriggerEnter(Collider collider) {
        environment.SetActive(true);
    }
}
