using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Find persistent sound manager
        SoundManager soundManager = FindFirstObjectByType<SoundManager>();

        // Set slider values from saved settings
        masterSlider.value =
            PlayerPrefs.GetFloat("MasterVolume", 1f);

        musicSlider.value =
            PlayerPrefs.GetFloat("MusicVolume", 1f);

        sfxSlider.value =
            PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Connect sliders
        masterSlider.onValueChanged.AddListener(soundManager.SetMasterVolume);

        musicSlider.onValueChanged.AddListener(soundManager.SetMusicVolume);

        sfxSlider.onValueChanged.AddListener(soundManager.SetSFXVolume);
    }
}
