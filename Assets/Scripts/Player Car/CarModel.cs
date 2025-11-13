using System;
using Enums;

[Serializable]
public class CarModel
{
    public event Action<bool> OnEngineStateChanged;
    public event Action<AutomaticGears> OnGearChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<float> OnAccelerationInputChanged;
    public event Action<float> OnBrakeInputChanged;
    public event Action<float> OnHandbrakeInputChanged;
    public event Action<float> OnSteerInputChanged;
    
    public bool IsStarted { get; private set; } = true;
    public bool Stationary { get; set; } = true;

    public float CurrentSpeed { get; private set; } = 0f;
    public float AccelerationInput { get; private set; } = 0f;
    public float BrakeInput { get; private set; } = 0f;
    public float HandbrakeInput { get; private set; } = 0f;
    public float SteerInput { get; private set; } = 0f;

    public float CurrentSteerAngle { get; set; } = 0f;
    public float TargetSteerAngle { get; set; } = 0f;
    public float FrontLeftWheelRPM { get; set; } = 0f;
    public float FrontRightWheelRPM { get; set; } = 0f;
    public float RearLeftWheelRPM { get; set; } = 0f;
    public float RearRightWheelRPM { get; set; } = 0f;

    public AutomaticGears CurrentGear { get; private set; } = AutomaticGears.Drive;

    public void SetEngineState(bool started)
    {
        if (IsStarted == started) return;
        IsStarted = started;
        OnEngineStateChanged?.Invoke(IsStarted);
    }

    public void SetGear(AutomaticGears gear)
    {
        if (CurrentGear == gear) return;
        CurrentGear = gear;
        OnGearChanged?.Invoke(CurrentGear);
    }

    public void SetSpeed(float speed)
    {
        if (Math.Abs(CurrentSpeed - speed) < 0.001f) return;
        CurrentSpeed = speed;
        OnSpeedChanged?.Invoke(CurrentSpeed);
    }

    public void SetAccelerationInput(float value)
    {
        if (Math.Abs(AccelerationInput - value) < 0.001f) return;
        AccelerationInput = value;
        OnAccelerationInputChanged?.Invoke(AccelerationInput);
    }

    public void SetBrakeInput(float value)
    {
        if (Math.Abs(BrakeInput - value) < 0.001f) return;
        BrakeInput = value;
        OnBrakeInputChanged?.Invoke(BrakeInput);
    }

    public void SetHandbrakeInput(float value)
    {
        if (Math.Abs(HandbrakeInput - value) < 0.001f) return;
        HandbrakeInput = value;
        OnHandbrakeInputChanged?.Invoke(HandbrakeInput);
    }

    public void SetSteerInput(float value)
    {
        if (Math.Abs(SteerInput - value) < 0.001f) return;
        SteerInput = value;
        OnSteerInputChanged?.Invoke(SteerInput);
    }
}
