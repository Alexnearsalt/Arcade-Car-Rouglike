using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text lapTimeText;
    
    private void Awake()
    {
        timeManager.LapEnded.AddListener(LapTimeDraw);
        
        lapTimeText.text = "---";
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
