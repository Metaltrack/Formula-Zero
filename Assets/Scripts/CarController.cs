using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Vehicle Stuff")]
    public float maxMotorTorque = 3000.0f;
    public float brakeTorque = 3000.0f;
    public float maxSteeringRange = 35.0f;
    public float steeringRangeAtMaxSpeed = 25.0f;
    public float maxSpeed = 40.0f;
    public float centerOfMassOffsetY = -1.0f;
    public bool isAI = false;

    private Rigidbody rigidBody;
    private WheelController[] wheels;
    private CarActions InputActions;
    private Vector2 input;
    public float AISteer = 0.0f;
    public bool steerByAngle = false;

    private void Awake()
    {
        InputActions = new CarActions();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    public void ApplyInput(float ForwardInput, float SteerInput)
    {
        input.x = SteerInput;
        input.y = ForwardInput;
    }

    public void ApplyInputAnalog(float steerAngle, float vIn = 0.0f)
    {
        steerAngle = Mathf.Clamp(steerAngle, -maxSteeringRange, maxSteeringRange);
        AISteer = steerAngle;
        input.y = vIn;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centerOfMassOffsetY;
        rigidBody.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAI)
        {
            input = InputActions.vehicleActions.Movement.ReadValue<Vector2>();
        }

        float hInput = input.x;
        float vInput = input.y;

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(maxMotorTorque, 0.0f, speedFactor);
        float steerAngle = Mathf.Lerp(maxSteeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);
        Console.WriteLine(input);
        foreach(var wheel in wheels)
        {
            if (wheel.steerable)
            {
                if (!isAI)
                {
                    wheel.wheelCollider.steerAngle = hInput * steerAngle;
                }
                else
                {
                    if (steerByAngle)
                    {
                        wheel.wheelCollider.steerAngle = AISteer;
                    }
                    else
                    {
                        wheel.wheelCollider.steerAngle = hInput * steerAngle;
                    }
                }
            }

            if (isAccelerating)
            {
                if (wheel.motorized)
                {
                    wheel.wheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.wheelCollider.brakeTorque = 0.0f;
            }
            else
            {
                wheel.wheelCollider.motorTorque = 0.0f;
                wheel.wheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }
    }
}
