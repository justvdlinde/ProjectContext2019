using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    private void OnValidate()
    {
        waypoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                child.name = "Waypoint " + (waypoints.Count + 1);
                waypoints.Add(child);
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.2f);

            if (i < waypoints.Count - 1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
