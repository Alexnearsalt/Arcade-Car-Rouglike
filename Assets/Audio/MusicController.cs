using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MuisicController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [Header("При сбросе слайдеров на UI перетащить в OnValueChanged GameObject \n с прекрепленным скриптом и там выбрать")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // private void Awake()
    // {
    //     LoadVolumes();
    // }

    private void OnEnable()
    {
        var data = VolumeSave.Load();
        if (data != null)
        {
            masterSlider.value = data.master;
            musicSlider.value = data.music;
            sfxSlider.value = data.sfx;
        }
    }
    public void SetMasterVolume()
    {
        var masterVolume = masterSlider.value;
        mixer.SetFloat("MasterVolume", masterVolume);
        SaveVolumes();
    }
    public void SetMusicVolume()
    {
        var musicVolume = musicSlider.value;
        mixer.SetFloat("MusicVolume", musicVolume);
        SaveVolumes();
    }
    public void SetSfxVolume()
    {
        var sfxVolume = sfxSlider.value;
        mixer.SetFloat("SfxVolume", sfxVolume);
        SaveVolumes();
    }
    
    private void SaveVolumes()
    {
        var data = new VolumeData
        {
            master = masterSlider.value,
            music = musicSlider.value,
            sfx = sfxSlider.value
        };

        VolumeSave.Save(data);
    }
}
