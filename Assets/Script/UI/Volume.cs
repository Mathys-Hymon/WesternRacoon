using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private float mainVolume = 0.5f;
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private AudioSource mainMusic;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MainVolume"))
        {
            mainVolume = PlayerPrefs.GetFloat("MainVolume");
        }

        mainVolumeSlider.value = mainVolume * 100;
        OnVolumeSlide();
    }
    

    public void OnVolumeSlide()
    {
        mainVolume = mainVolumeSlider.value / 100;
        mainMusic.volume = mainVolume;
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
    }
}
