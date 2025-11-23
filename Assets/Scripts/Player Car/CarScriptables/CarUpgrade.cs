using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade0", menuName = "Scriptable Objects/Upgrade0")]
public class CarUpgrade : ScriptableObject
{
    [Header("умножение")]
    [SerializeField] private float maxForwardSpeedMultiplier = 1f;
    [SerializeField] private float horsePowerMultiplier = 1f;
    [SerializeField] private float brakePowerMultiplier = 1f;
    [SerializeField] private float handbrakeForceMultiplier = 1f;

    [Header("Чложение")]
    [SerializeField] private float maxForwardSpeedAdd = 0f;
    [SerializeField] private float horsePowerAdd = 0f;

    public float MaxForwardSpeedMultiplier => maxForwardSpeedMultiplier;
    public float HorsePowerMultiplier => horsePowerMultiplier;
    public float BrakePowerMultiplier => brakePowerMultiplier;
    public float HandbrakeForceMultiplier => handbrakeForceMultiplier;

    public float MaxForwardSpeedAdd => maxForwardSpeedAdd;
    public float HorsePowerAdd => horsePowerAdd;

    public void Apply(CarStatsRuntime stats)
    {
        stats.MaxForwardSpeed = stats.MaxForwardSpeed * MaxForwardSpeedMultiplier + MaxForwardSpeedAdd;
        stats.HorsePower = stats.HorsePower * HorsePowerMultiplier + HorsePowerAdd;

        stats.BrakePower *= BrakePowerMultiplier;
        stats.HandbrakeForce *= HandbrakeForceMultiplier;
    }
}
