using UnityEngine;
using UnityEngine.Audio;

public class VolumeBootstrap : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        var data = VolumeSave.Load();
        if (data == null) return;
        
        mixer.SetFloat("MasterVolume", data.master);
        mixer.SetFloat("MusicVolume", data.music);
        mixer.SetFloat("SfxVolume", data.sfx);
    }
}
