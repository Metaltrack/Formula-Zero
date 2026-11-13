using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private CarController vehicleController;
    public Vector2 debugInput;
    public bool debug = false;
    public float steerSense = 4.0f;

    [Header("AI Handler stuff")]
    public WaypointHandler waypointContainer;
    public List<waypoint> waypoints;
    public int currentWaypoint = 0;
    private float currentAngle;
    private float vIn;
    private Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vehicleController = GetComponent<CarController>();
        foreach(waypoint point in waypointContainer.GetComponentsInChildren<waypoint>())
        {
            waypoints.Add(point);
        }

        color = new Color();
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            vehicleController.ApplyInput(debugInput.x, debugInput.y);
            return;
        }

        if (
            Vector3.Distance(waypoints[currentWaypoint].waypointData.waypointTransform.position, transform.position) < 
            waypoints[currentWaypoint].waypointData.distanceToArrive
            )
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Count) currentWaypoint = 0;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        currentAngle = Mathf.Lerp(currentAngle, Vector3.SignedAngle(forward, waypoints[currentWaypoint].waypointData.waypointTransform.position - transform.position, Vector3.up), steerSense * Time.deltaTime);

        if (waypoints[currentWaypoint].waypointData.isAtEndOfBrakingZone)
        {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].waypointData.waypointTransform.position) < waypoints[currentWaypoint].waypointData.brakingZoneLength)
            {
                vIn = -4.0f;
                color = Color.red;
            }
        }
        else
        {
            vIn = 1.0f;
            color = Color.yellow;
        }

        Debug.DrawRay(transform.position, waypoints[currentWaypoint].waypointData.waypointTransform.position - transform.position, color);
        vehicleController.ApplyInputAnalog(currentAngle, vIn);
    }
}
