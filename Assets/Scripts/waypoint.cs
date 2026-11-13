using UnityEngine;

public class waypoint : MonoBehaviour
{
    public bool is_active = true;
    public bool is_end_of_braking_zone = false;
    public float braking_zone_length = 0.0f;

    public struct WaypointData
    {
        public bool isActive;
        public Transform waypointTransform;
        public bool isAtEndOfBrakingZone;
        public float brakingZoneLength;
        public float distanceToArrive;

        public WaypointData(
            bool is_active,
            Transform waypoint_transform,
            bool is_at_end_of_braking_zone,
            float braking_zone_len,
            float distance_to_arrive
            )
        {
            isActive = is_active;
            waypointTransform = waypoint_transform;
            isAtEndOfBrakingZone = is_at_end_of_braking_zone;
            brakingZoneLength = braking_zone_len;
            distanceToArrive = distance_to_arrive;
        }
    }

    public WaypointData waypointData;

    private void Awake()
    {
        waypointData = new WaypointData(is_active, transform, is_end_of_braking_zone, braking_zone_length, 15.0f);
    }
}
