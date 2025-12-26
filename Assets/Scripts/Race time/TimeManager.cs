using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private int trackID;
    [SerializeField] private List<Checkpoint> checkpoints;
    public UnityEvent LapEnded;
    public UnityEvent CheckpointReached;

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

    public int CheckpointsAmount
    {
        get => checkpoints.Count;
    }

    public int CurrentCheckpoint
    {
        get => currentCheckpoint;
    }
    
    private void Awake()
    {
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.TriggerEntered.AddListener(CheckpointTriggered);
            checkpoint.gameObject.SetActive(false);
        }
        
        checkpoints.ElementAt(0).gameObject.SetActive(true);
    }

    private void CheckpointTriggered(Checkpoint checkpoint)
    {
        // if (checkpoint.ID == 1)
        // {
        //     OnLapEnded();
        // }
        
        if (checkpoint.ID == currentCheckpoint + 1)
        {
            if (checkpoint.ID == 0)
            {
                startTime = Time.time;
                currentCheckpoint = 0;
                CheckpointReached.Invoke();
                
                checkpoints.ElementAt(currentCheckpoint).gameObject.SetActive(false);
                checkpoints.ElementAt(currentCheckpoint +1).gameObject.SetActive(true);
            }

            else
            {
                if (checkpoint.ID == checkpoints.Last().ID)
                {
                    currentCheckpoint++;
                    CheckpointReached.Invoke();
                    // Debug.Log(Time.time - startTime);
                    
                    checkpoints.ElementAt(currentCheckpoint).gameObject.SetActive(false);
                    checkpoints.ElementAt(0).gameObject.SetActive(true);
                }

                else
                {
                    
                    currentCheckpoint++;
                    CheckpointReached.Invoke();
                    // Debug.Log(Time.time - startTime);
                
                    checkpoints.ElementAt(currentCheckpoint).gameObject.SetActive(false);
                    checkpoints.ElementAt(currentCheckpoint +1).gameObject.SetActive(true);
                }
            }
            
        }
        
        else
        {
            if (checkpoint.ID == 0 && currentCheckpoint == checkpoints.Last().ID)
            {
                OnLapEnded();
                currentCheckpoint++;
                CheckpointReached.Invoke();
                // Debug.Log(lapTime);
                
                checkpoints.ElementAt(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnLapEnded()
    {
        lapTime = Time.time - startTime;
        LapEnded.Invoke();
        var tracksData = GameLoadSave.gameState.tracksData;
        var track = tracksData.FirstOrDefault(x => x.trackID == trackID);
        if (track.isTimeSet)
        {
            if (track.bestLapTime > lapTime)
                track.bestLapTime = lapTime;
        }
        else
        {
            track.isTimeSet = true;
            track.bestLapTime = lapTime;
        }
        
        GameLoadSave.SaveState();
        
        //Coroutine затемнение
        SceneManager.LoadScene("Levels Menu");
    }

    private void Update()
    {
        if (startTime > 0)
        {
            currentTime = Time.time - startTime;
        }
    }
}
