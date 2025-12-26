using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private CarFactory carFactory;

    private void Start()
    {
        carFactory.CreatePlayerCar();
    }
}
