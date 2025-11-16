using UnityEngine;
using UnityEngine.Events;


public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int CheckpointID;
    private int currentCar;

    public int CurrentCar
    {
        get => currentCar;
        set => currentCar = value;
    }
    
    public UnityEvent TriggerEntered;

    private void OnTriggerEnter(Collider collider)
    {
        var carBody = collider.GetComponent<CarBody>();
    
        if (carBody is not null)
        {
            CurrentCar = carBody.ID;
            TriggerEntered.Invoke();
            //return;
        }
    }
}