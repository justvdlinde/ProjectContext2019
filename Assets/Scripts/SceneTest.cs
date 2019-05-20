using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour
{
    [SerializeField, Scene] private string scene;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject greatHallObject;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 10, 200, 50), "Load normal")) 
        {
            SceneManager.LoadScene(scene);
        }
        if (GUI.Button(new Rect(200, 10, 200, 50), "Load additive"))
        {
            SceneManager.LoadSceneAsync(scene);
        }
        if (GUI.Button(new Rect(400, 10, 200, 50), "Menu"))
        {
            SceneManager.LoadSceneAsync(0);
        }

        if (GUI.Button(new Rect(0, 60, 200, 50), "Great Hall"))
        {
            menuObject.SetActive(false);
            greatHallObject.SetActive(true);
        }
        if (GUI.Button(new Rect(200, 60, 200, 50), "Menu"))
        {
            greatHallObject.SetActive(false);
            menuObject.SetActive(true);
        }
    }
}
