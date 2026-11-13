using UnityEngine;

public class WheelController : MonoBehaviour
{
    public Transform wheelModel;
    [HideInInspector] public WheelCollider wheelCollider;

    public bool steerable;
    public bool motorized;

    Vector3 position;
    Quaternion rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }
}
