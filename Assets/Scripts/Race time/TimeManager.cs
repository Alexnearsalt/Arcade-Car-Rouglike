using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;
    private int currentCheckpoint = -1;
    private float startTime;
    private float lapTime;
    // public UnityEvent NewCheckpointRiched;

    private void Awake()
    {
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.TriggerEntered.AddListener(CheckpointTriggered);
        }
    }

    private void CheckpointTriggered(Checkpoint checkpoint)
    {
        if (checkpoint.ID == currentCheckpoint + 1)
        {
            if (checkpoint.ID == 0)
            {
                startTime = Time.time;
                currentCheckpoint = 0;
            }
            
            else
            {
                currentCheckpoint = checkpoint.ID;
                Debug.Log(Time.time - startTime);
            }
        }
        
        else
        {
            if (checkpoint.ID == 0 && currentCheckpoint == checkpoints.Last().ID)
            {
                lapTime = Time.time - startTime;
                Debug.Log(lapTime);
            }
        }
    }
}
