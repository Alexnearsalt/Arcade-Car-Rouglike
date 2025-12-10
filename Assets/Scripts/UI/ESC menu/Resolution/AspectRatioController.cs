using System;
using UnityEngine;
using TMPro;

public class AspectRatioController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private const string PREF_KEY = "last_resolution_index";

    private void Awake()
    {
        if (resolutionDropdown == null)
            resolutionDropdown = GetComponent<TMP_Dropdown>();
        
        var savedIndex = PlayerPrefs.GetInt(PREF_KEY, 0); 

        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
        
        ApplyResolutionByIndex(savedIndex);
    }

    private void OnEnable()
    {
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void OnDisable()
    {
        if (resolutionDropdown != null)
            resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
    }

    private void OnResolutionChanged(int index)
    {
        ApplyResolutionByIndex(index);
        PlayerPrefs.SetInt(PREF_KEY, index);
        PlayerPrefs.Save();
    }

    private void ApplyResolutionByIndex(int index)
    {
        switch (index)
        {
            case 0: // 1920x1080 (16:9)
                SetResolution(1920, 1080);
                break;

            case 1: // 2560x1080 (21:9)
                SetResolution(2560, 1080);
                break;

            case 2: // 1280x720 (16:9)
                SetResolution(1280, 720);
                break;

            case 3: // 1680x720 (21:9)
                SetResolution(1680, 720);
                break;
        }
    }

    private void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
        Debug.Log($"[AspectRatioController] Resolution set to {width}x{height}");
    }
}
