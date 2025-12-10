using UnityEngine;

public class ResolutionBootstrap : MonoBehaviour
{
    private const string PREF_KEY = "last_resolution_index";

    private void Awake()
    {
        ApplySavedResolution();
    }

    public void ApplySavedResolution()
    {
        int index = PlayerPrefs.GetInt(PREF_KEY, 0);
        ApplyResolutionByIndex(index);
    }

    private void ApplyResolutionByIndex(int index)
    {
        switch (index)
        {
            case 0: 
                SetResolution(1920, 1080);
                break;

            case 1: 
                SetResolution(2560, 1080);
                break;

            case 2: 
                SetResolution(1280, 720);
                break;

            case 3: 
                SetResolution(1680, 720);
                break;

            default:
                SetResolution(1920, 1080);
                break;
        }
    }

    private void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
        Debug.Log($"[ResolutionBootstrap] Resolution set to {width}x{height}");
    }
}

