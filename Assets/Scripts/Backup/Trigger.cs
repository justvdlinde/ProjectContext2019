using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Action<Collider> TriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent?.Invoke(other);
    }
}
