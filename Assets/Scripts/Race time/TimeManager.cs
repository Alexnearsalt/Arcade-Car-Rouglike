using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;

    public UnityEvent NewCheckpointRiched;
    
}
