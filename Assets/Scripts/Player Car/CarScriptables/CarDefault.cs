using UnityEngine;

[CreateAssetMenu(fileName = "CarDefault", menuName = "Scriptable Objects/CarDefault")]
public class CarDefault : ScriptableObject
{
    [SerializeField] private float maxForwardSpeed = 100f;
    [SerializeField] private float maxReverseSpeed = 30f;
    [SerializeField] private float horsePower = 1000f;
    [SerializeField] private float brakePower = 2000f;
    [SerializeField] private float handbrakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private float steeringSpeed = 5f;
    [SerializeField] private float decelerationSpeed = 0.5f;

    public float MaxForwardSpeed => maxForwardSpeed;
    public float MaxReverseSpeed => maxReverseSpeed;
    public float HorsePower => horsePower;
    public float BrakePower => brakePower;
    public float HandbrakeForce => handbrakeForce;
    public float MaxSteerAngle => maxSteerAngle;
    public float SteeringSpeed => steeringSpeed;
    public float DecelerationSpeed => decelerationSpeed;
}
