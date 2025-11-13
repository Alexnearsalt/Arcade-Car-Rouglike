using UnityEngine;
using Enums;

public class CarController : MonoBehaviour
{
    [Header("References")] 
    public Rigidbody vehicleRB;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    [Header("Settings")] 
    public float maxForwardSpeed = 100f;
    public float maxReverseSpeed = 30f;
    public float horsePower = 1000f;
    public float brakePower = 2000f;
    public float handbrakeForce = 3000f;
    public float maxSteerAngle = 30f;
    public float steeringSpeed = 5f;
    public float stopThreshold = 1f;
    public float decelerationSpeed = 0.5f;

    [Header("Drive Type")] 
    public DriveTypes driveType = DriveTypes.RWD;
    
    public CarModel Model { get; private set; } = new CarModel();

    private WheelCollider[] _wheels;

    private void Awake()
    {
        _wheels = new WheelCollider[]
        {
            frontLeftWheelCollider,
            frontRightWheelCollider,
            rearLeftWheelCollider,
            rearRightWheelCollider
        };

        if (vehicleRB == null)
        {
            Debug.LogError("VehicleRB reference is missing for CarController!");
        }
    }

    public void ToggleEngine()
    {
        bool newState = !Model.IsStarted;
        Model.SetEngineState(newState);

        if (!newState)
        {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;
        }
    }

    public void SetAcceleration(float value) => Model.SetAccelerationInput(value);
    public void SetBrake(float value)        => Model.SetBrakeInput(value);
    public void SetHandbrake(float value)    => Model.SetHandbrakeInput(value);

    public void SetSteer(float value)
    {
        Model.TargetSteerAngle = value * maxSteerAngle;
        Model.SetSteerInput(value);
    }

    public void ShiftUp()
    {
        switch (Model.CurrentGear)
        {
            case AutomaticGears.Reverse:
                Model.SetGear(AutomaticGears.Neutral);
                break;
            case AutomaticGears.Neutral:
                Model.SetGear(AutomaticGears.Drive);
                break;
        }
    }

    public void ShiftDown()
    {
        switch (Model.CurrentGear)
        {
            case AutomaticGears.Drive:
                Model.SetGear(AutomaticGears.Neutral);
                break;
            case AutomaticGears.Neutral:
                Model.SetGear(AutomaticGears.Reverse);
                break;
        }
    }

    private void FixedUpdate()
    {
        HandleAcceleration();
        HandleBraking();
        HandleHandbrake();
        HandleSteering();
        HandleSlowdown();
        UpdateSpeedAndStationary();
        UpdateWheelRpm();
    }

    private void HandleAcceleration()
    {
        if (!Model.IsStarted) return;

        float acc = Model.AccelerationInput;
        float currentSpeed = Model.CurrentSpeed;

        if (Model.CurrentGear == AutomaticGears.Drive)
        {
            float speedFactor = Mathf.InverseLerp(0, maxForwardSpeed, currentSpeed);
            float currentMotorTorque = Mathf.Lerp(horsePower, 0, speedFactor);

            if (acc > 0f && currentSpeed < maxForwardSpeed)
                ApplyMotorTorque(currentMotorTorque * acc);
            else
                ApplyMotorTorque(0f);
        }
        else if (Model.CurrentGear == AutomaticGears.Reverse)
        {
            if (acc > 0f && currentSpeed > -maxReverseSpeed)
                ApplyMotorTorque(-horsePower);
            else
                ApplyMotorTorque(0f);
        }
    }

    private void ApplyMotorTorque(float torque)
    {
        switch (driveType)
        {
            case DriveTypes.RWD:
                rearLeftWheelCollider.motorTorque = torque;
                rearRightWheelCollider.motorTorque = torque;
                frontLeftWheelCollider.motorTorque = 0;
                frontRightWheelCollider.motorTorque = 0;
                break;

            case DriveTypes.FWD:
                frontLeftWheelCollider.motorTorque = torque;
                frontRightWheelCollider.motorTorque = torque;
                rearLeftWheelCollider.motorTorque = 0;
                rearRightWheelCollider.motorTorque = 0;
                break;

            case DriveTypes.AWD:
                frontLeftWheelCollider.motorTorque = torque;
                frontRightWheelCollider.motorTorque = torque;
                rearLeftWheelCollider.motorTorque = torque;
                rearRightWheelCollider.motorTorque = torque;
                break;
        }
    }

    private void HandleBraking()
    {
        float brake = Model.BrakeInput;

        if (brake > 0f)
        {
            float torque = brake * brakePower;
            frontLeftWheelCollider.brakeTorque = torque;
            frontRightWheelCollider.brakeTorque = torque;
        }
        else
        {
            frontLeftWheelCollider.brakeTorque = 0;
            frontRightWheelCollider.brakeTorque = 0;
        }
    }

    private void HandleHandbrake()
    {
        float hb = Model.HandbrakeInput;

        if (hb > 0f)
        {
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;

            float torque = hb * handbrakeForce;
            rearLeftWheelCollider.brakeTorque = torque;
            rearRightWheelCollider.brakeTorque = torque;
        }
        else
        {
            rearLeftWheelCollider.brakeTorque = 0;
            rearRightWheelCollider.brakeTorque = 0;
        }
    }

    private void HandleSteering()
    {
        float currentSpeed = Model.CurrentSpeed;
        float adjustedSpeedFactor = Mathf.InverseLerp(20, maxForwardSpeed, currentSpeed);
        float adjustedTurnAngle = Model.TargetSteerAngle * (1 - adjustedSpeedFactor);

        Model.CurrentSteerAngle = Mathf.Lerp(
            Model.CurrentSteerAngle,
            adjustedTurnAngle,
            Time.deltaTime * steeringSpeed
        );

        frontLeftWheelCollider.steerAngle = Model.CurrentSteerAngle;
        frontRightWheelCollider.steerAngle = Model.CurrentSteerAngle;
    }

    private void HandleSlowdown()
    {
        if (vehicleRB == null) return;

        if (Model.AccelerationInput == 0 &&
            Model.BrakeInput == 0 &&
            Model.HandbrakeInput == 0)
        {
#if UNITY_6000_0_OR_NEWER
            vehicleRB.linearVelocity =
                Vector3.Lerp(vehicleRB.linearVelocity, Vector3.zero, Time.deltaTime * decelerationSpeed);
#else
            vehicleRB.velocity =
                Vector3.Lerp(vehicleRB.velocity, Vector3.zero, Time.deltaTime * decelerationSpeed);
#endif
        }
    }

    private void UpdateSpeedAndStationary()
    {
        if (vehicleRB != null)
        {
#if UNITY_6000_0_OR_NEWER
            float speed = Vector3.Dot(vehicleRB.transform.forward, vehicleRB.linearVelocity) * 3.6f;
#else
            float speed = Vector3.Dot(vehicleRB.transform.forward, vehicleRB.velocity) * 3.6f;
#endif
            Model.SetSpeed(speed);
        }

        bool inStop =
            Mathf.Abs(frontLeftWheelCollider.rpm) < stopThreshold &&
            Mathf.Abs(frontRightWheelCollider.rpm) < stopThreshold &&
            Mathf.Abs(rearLeftWheelCollider.rpm) < stopThreshold &&
            Mathf.Abs(rearRightWheelCollider.rpm) < stopThreshold;

        Model.Stationary = inStop;
    }

    private void UpdateWheelRpm()
    {
        Model.FrontLeftWheelRPM = frontLeftWheelCollider.rpm;
        Model.FrontRightWheelRPM = frontRightWheelCollider.rpm;
        Model.RearLeftWheelRPM = rearLeftWheelCollider.rpm;
        Model.RearRightWheelRPM = rearRightWheelCollider.rpm;
    }

    public bool InAir()
    {
        foreach (WheelCollider wheel in _wheels)
        {
            if (wheel != null && wheel.GetGroundHit(out _))
                return false;
        }

        return true;
    }
}
