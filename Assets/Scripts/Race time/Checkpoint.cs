using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private OnTriggerEnter(Collider collider)
    {
        var base = collider.TryGetComponent<Base>();

        if (base is not null)
        {
            base.TriggerEnter();
        }
    }
}