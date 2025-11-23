using System.Collections.Generic;
using UnityEngine;

public class CarUpgradeManager : MonoBehaviour
{
    [SerializeField] private CarDefault baseStats;
    [SerializeField] private List<CarUpgrade> activeUpgrades = new List<CarUpgrade>();

    public CarStatsRuntime RuntimeStats { get; private set; }

    private CarController carController;

    private void Awake()
    {
        if (baseStats == null)
        {
            baseStats = GetComponent<CarDefault>();
        }
        carController = GetComponent<CarController>();
        RecalculateStats();
    }

    public void AddUpgrade(CarUpgrade upgrade)
    {
        activeUpgrades.Add(upgrade);
        RecalculateStats();
    }

    public void RemoveUpgrade(CarUpgrade upgrade)
    {
        activeUpgrades.Remove(upgrade);
        RecalculateStats();
    }

    public void RecalculateStats()
    {
        // 1) создаём копию базовых статов
        RuntimeStats = new CarStatsRuntime(baseStats);

        // 2) применяем каждый апгрейд
        foreach (var upgrade in activeUpgrades)
        {
            upgrade?.Apply(RuntimeStats);
        }

        // 3) передаём итоговое значение в машину
        carController.ApplyStats(RuntimeStats);
    }
}
