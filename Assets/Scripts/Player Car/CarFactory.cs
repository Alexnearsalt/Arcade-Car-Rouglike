using UnityEngine;

public class CarFactory : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform spawnPoint;

    public CarController CreatePlayerCar()
    {
        var carGO = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

        var carController = carGO.GetComponent<CarController>();
        var upgradeManager = carGO.GetComponent<CarUpgradeManager>();

        if (upgradeManager != null)
        {
            //upgradeManager.InitializeFromProgress();
        }

        return carController;
    }
}
