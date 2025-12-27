using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UIElements;


public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private int unlockPrice2;
    [SerializeField] private int unlockPrice3;
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
    
    private ProgressController _progress;
    private GameState tracksData;
    private TrackData track1;
    private TrackData track2;
    private TrackData track3;

    private void Awake()
    {
        _progress = ProgressController.Instance;
        GameLoadSave.Initialize();
        RedrawMenu();
        var tracksData = GameLoadSave.gameState.tracksData;
        var track1 = tracksData.FirstOrDefault(x => x.trackID == 1);
        var track2 = tracksData.FirstOrDefault(x => x.trackID == 2);
        var track3 = tracksData.FirstOrDefault(x => x.trackID == 3);
        
        if (track2.isUnlocked)
            locker2.SetActive(false);
        
        if (track3.isUnlocked)
            locker3.SetActive(false);
        
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
        SceneManager.LoadScene("Second Track");
    }

    private void OnClickPlay3()
    {
        Debug.Log("Play track 3");
    }

    private void OnClickUnlock2()
    {
        if (_progress.Model.TrySpendCoins(unlockPrice2))
        {
            locker2.SetActive(false);
            track2.isUnlocked = true;
        }
    }

    private void OnClickUnlock3()
    {
        if (_progress.Model.TrySpendCoins(unlockPrice3))
        {
            locker3.SetActive(false);
            track3.isUnlocked = true;
        }
    }

    private void RedrawMenu()
    {
        if (track1.isTimeSet)
        {
            textBest1.text = TimeSpan.FromSeconds(track1.bestLapTime).ToString("m\\:ss\\.fff");
            textStars1.text = string.Format("{0}/3", track1.stars);
        }
        
        if (track2.isTimeSet)
        {
            textBest2.text = TimeSpan.FromSeconds(track2.bestLapTime).ToString("m\\:ss\\.fff");
            textStars2.text = string.Format("{0}/3", track2.stars);
        }
        
        if (track3.isTimeSet)
        {
            textBest3.text = TimeSpan.FromSeconds(track3.bestLapTime).ToString("m\\:ss\\.fff");
            textStars3.text = string.Format("{0}/3", track3.stars);
        }
    }
    
}