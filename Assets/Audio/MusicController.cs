using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//TODO: реализовать JSON сохранения параметров звука
public class MuisicController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    //При сбросе слайдеров на UI перетащить в OnValueChanged GameObject с прекрепленным скриптом и там выбрать 
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        LoadVolumes();
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
    private void LoadVolumes()
    {
        var data = VolumeSave.Load();
        if (data == null) return;

        masterSlider.value = data.master;
        musicSlider.value = data.music;
        sfxSlider.value = data.sfx;

        mixer.SetFloat("MasterVolume", data.master);
        mixer.SetFloat("MusicVolume", data.music);
        mixer.SetFloat("SfxVolume", data.sfx);
    }
}
