using UnityEngine;

public class RoomExitTrigger : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ARManager arManager;
    [SerializeField] private MenuScreen menuScreen;

    public void OnTriggerEnter(Collider other)
    {
        menuScreen.ShowMenu(true);
        arManager.EnableAR(false);
    }
}
