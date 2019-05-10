using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] [LocationID] private int id;
    public int ID => id;
    public GameObject GameObject { get { return gObject; } }
    public Collider Collider { get { return collider; } }

    [SerializeField, HideInInspector] private GameObject gObject;
    [SerializeField, HideInInspector] private new Collider collider;

    private void OnValidate()
    {
        gObject = gameObject;
        collider = GetComponent<Collider>();
    }
}
