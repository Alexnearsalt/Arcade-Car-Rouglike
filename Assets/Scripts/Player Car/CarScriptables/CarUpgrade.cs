using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade0", menuName = "Scriptable Objects/Upgrade0")]
public class CarUpgrade : ScriptableObject
{
    [Header("Магаз")]
    [SerializeField] private string upgradeId = "upgrade_0";
    [SerializeField] private string displayName = "Upgrade";
    [TextArea, SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [Min(0), SerializeField] private int price = 0;
    
    [Header("умножение")]
    [SerializeField] private float maxForwardSpeedMultiplier = 1f;
    [SerializeField] private float horsePowerMultiplier = 1f;
    [SerializeField] private float brakePowerMultiplier = 1f;
    [SerializeField] private float handbrakeForceMultiplier = 1f;

    [Header("Сложение")]
    [SerializeField] private float maxForwardSpeedAdd = 0f;
    [SerializeField] private float horsePowerAdd = 0f;

    public string UpgradeId => upgradeId;
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public int Price => price;
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
