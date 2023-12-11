using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private float volume = 0.5f;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        volumeSlider.value = volume;
        OnVolumeSlide();
    }
    public void OnVolumeSlide()
    {
        volume = volumeSlider.value;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
