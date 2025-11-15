using UnityEngine;
using UnityEngine.Events;

public class CarBase : MonoBehaviour
{
    [SerializeField] private int carID;
    
    public int ID => carID;

    public UnityEvent<CarBase> TriggerEntered;

    public void TriggerEnter()
    {
        TriggerEntered.Invoke(this);
    }
}