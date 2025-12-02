using UnityEngine;

[CreateAssetMenu(fileName = "MusicSettings", menuName = "Scriptable Objects/MusicSettings")]
public class MusicSettings : ScriptableObject
{
    [SerializeField] private float masterVolume;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;

    public float MasterVolume
    {
        get { return masterVolume;}
        set { masterVolume = value; }
    }
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
}
