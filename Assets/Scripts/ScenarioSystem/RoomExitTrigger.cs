﻿using UnityEngine;
using ServiceLocatorNamespace;

public class RoomExitTrigger : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private MenuScreen menuScreen;

    public void OnTriggerEnter(Collider other)
    {
        menuScreen.ShowMenu(true);
        (ServiceLocator.Instance.Get<ARManagerService>() as ARManagerService).EnableAR(false);
    }
}
