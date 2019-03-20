using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Action<Collider> TriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent?.Invoke(other);
    }
}
