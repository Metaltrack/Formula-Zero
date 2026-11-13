using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    public List<waypoint> waypoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(waypoint point in gameObject.GetComponentsInChildren<waypoint>())
        {
            waypoints.Add(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
