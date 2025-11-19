using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;
    public UnityEvent LapEnded;
    private int currentCheckpoint = -1;
    private float currentTime;
    private float startTime;
    private float lapTime;
    public float CurrentTime
    {
        get => currentTime;
    }

    public float LapTime
    {
        get => lapTime;
    }
    
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
                // Debug.Log(Time.time - startTime);
            }
        }
        
        else
        {
            if (checkpoint.ID == 0 && currentCheckpoint == checkpoints.Last().ID)
            {
                lapTime = Time.time - startTime;
                LapEnded.Invoke();
                // Debug.Log(lapTime);
            }
        }
    }

    private void Update()
    {
        if (startTime > 0)
        {
            currentTime = Time.time - startTime;
        }
    }
}
