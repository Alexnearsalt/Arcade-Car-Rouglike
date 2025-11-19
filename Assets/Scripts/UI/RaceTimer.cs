using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text lapTimeText;
    [SerializeField] private TMP_Text checkpointText;
    
    private void Awake()
    {
        timeManager.LapEnded.AddListener(LapTimeDraw);
        timeManager.CheckpointReached.AddListener(CheckpointRedraw);
        
        lapTimeText.text = "---";
        checkpointText.text = "---";
    }
    
    private void CheckpointRedraw()
    {
        checkpointText.text = string.Format("{0}/{1}", timeManager.CurrentCheckpoint, timeManager.CheckpointsAmount);
    }

    private void LapTimeDraw()
    {
        lapTimeText.text = TimeSpan.FromSeconds(timeManager.LapTime).ToString("m\\:ss\\.fff");
    }
    
    private void Update()
    {
        timeText.text = TimeSpan.FromSeconds(timeManager.CurrentTime).ToString("m\\:ss\\.fff");

        //TODO: перенести в метод по ивенту
        // if (timeManager.laptime > 0)
        // {
        //     lapTimeText.text = TimeSpan.FromSeconds(timeManager.LapTime).ToString("m\\:ss\\.fff");
        // }
    }
}
