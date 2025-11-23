using System;
using UnityEngine;

[Serializable]
public class CarStatsRuntime
{
    public float MaxForwardSpeed { get; set; }
    public float MaxReverseSpeed { get; set; }
    public float HorsePower { get; set; }
    public float BrakePower { get; set; }
    public float HandbrakeForce { get; set; }
    public float MaxSteerAngle { get; set; }
    public float SteeringSpeed { get; set; }
    public float DecelerationSpeed { get; set; }

    public CarStatsRuntime(CarDefault stats)
    {
        MaxForwardSpeed = stats.MaxForwardSpeed;
        MaxReverseSpeed = stats.MaxReverseSpeed;
        HorsePower = stats.HorsePower;
        BrakePower = stats.BrakePower;
        HandbrakeForce = stats.HandbrakeForce;
        MaxSteerAngle = stats.MaxSteerAngle;
        SteeringSpeed = stats.SteeringSpeed;
        DecelerationSpeed = stats.DecelerationSpeed;
    }
}

