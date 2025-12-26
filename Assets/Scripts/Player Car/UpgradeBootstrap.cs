using UnityEngine;

public class UpgradeBootstrap : MonoBehaviour
{
    private void Awake()
    {
        var car = FindObjectOfType<CarUpgradeManager>();
        if (car == null)
        {
            Debug.LogError("UpgradeBootstrap: CarUpgradeManager not found");
            return;
        }

        //car.InitializeFromProgress();
    }
}
