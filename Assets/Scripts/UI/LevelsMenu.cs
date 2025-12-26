using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;


public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonPlay1;
    [SerializeField] private Button buttonPlay2;
    [SerializeField] private Button buttonPlay3;
    [SerializeField] private Button buttonUnlock2;
    [SerializeField] private Button buttonUnlock3;
    [SerializeField] private TMP_Text textBest1;
    [SerializeField] private TMP_Text textStars1;
    [SerializeField] private TMP_Text textBest2;
    [SerializeField] private TMP_Text textStars2;
    [SerializeField] private TMP_Text textBest3;
    [SerializeField] private TMP_Text textStars3;
    [SerializeField] private GameObject locker1;
    [SerializeField] private GameObject locker2;
    [SerializeField] private GameObject locker3;
    
    
    private void Awake()
    {
        GameLoadSave.Initialize();
        RedrawMenu();
        
        buttonBack.onClick.AddListener(OnClickBack);
        buttonPlay1.onClick.AddListener(OnClickPlay1);
        buttonPlay2.onClick.AddListener(OnClickPlay2);
        buttonPlay3.onClick.AddListener(OnClickPlay3);
        buttonUnlock2.onClick.AddListener(OnClickUnlock2);
        buttonUnlock3.onClick.AddListener(OnClickUnlock3);
    }


    private void OnClickBack()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    private void OnClickPlay1()
    {
        SceneManager.LoadScene("First Track");
    }

    private void OnClickPlay2()
    {
        Debug.Log("Play 2");
    }

    private void OnClickPlay3()
    {
        
    }

    private void OnClickUnlock2()
    {
        Debug.Log("Unlock 2");
    }

    private void OnClickUnlock3()
    {
        
    }

    private void RedrawMenu()
    {
        var tracksData = GameLoadSave.gameState.tracksData;
        var track1 = tracksData.FirstOrDefault(x => x.trackID == 1);
        var track2 = tracksData.FirstOrDefault(x => x.trackID == 2);
        var track3 = tracksData.FirstOrDefault(x => x.trackID == 3);
        
        if (track1.isTimeSet)
        {
            textBest1.text = TimeSpan.FromSeconds(track1.bestLapTime).ToString("m\\:ss\\.fff");
            textStars1.text = string.Format("{0}/3", track1.stars);
        }
        
        if (track2.isTimeSet)
        {
            textBest1.text = TimeSpan.FromSeconds(track2.bestLapTime).ToString("m\\:ss\\.fff");
            textStars1.text = string.Format("{0}/3", track2.stars);
        }
        
        if (track3.isTimeSet)
        {
            textBest1.text = TimeSpan.FromSeconds(track3.bestLapTime).ToString("m\\:ss\\.fff");
            textStars1.text = string.Format("{0}/3", track3.stars);
        }
    }
    
}