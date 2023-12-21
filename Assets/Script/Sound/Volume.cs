using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [Header("MainMusic")]
    [SerializeField] private float mainVolume;
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private AudioSource mainMusic;
    
    [Header("SFX")]
    [SerializeField] private float sfxVolume;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioSource sfxMusic;
    
    [SerializeField] private float masterVolume;
    

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MainVolume"))
        {
            mainVolume = PlayerPrefs.GetFloat("MainVolume");
        }
        
        mainVolumeSlider.value = mainVolume * 100;
        
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        
        sfxSlider.value = sfxVolume * 100;
        OnVolumeSlide();
    }

    public void OnVolumeSlide()
    {
        mainVolume = mainVolumeSlider.value / 100;
        mainMusic.volume = mainVolume;
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
        
        sfxVolume = sfxSlider.value / 100;
        sfxMusic.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
}
