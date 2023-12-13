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
        volumeSlider.value = volume;
        OnVolumeSlide();
    }

    public void OnVolumeSlide()
    {
        volume = volumeSlider.value / 100;
        audioSource.volume = volume;
    }
}
