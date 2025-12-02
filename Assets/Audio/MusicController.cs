using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//TODO: реализовать JSON сохранения параметров звука
public class MuisicController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void SetMasterVolume()
    {
        var masterVolume = masterSlider.value;
        mixer.SetFloat("MasterVolume", masterVolume);
    }
    public void SetMusicVolume()
    {
        var musicVolume = musicSlider.value;
        mixer.SetFloat("MusicVolume", musicVolume);
    }
    public void SetSfxVolume()
    {
        var sfxVolume = sfxSlider.value;
        mixer.SetFloat("SfxVolume", sfxVolume);
    }
}
