using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = MusicManager.MusicVolume;
        sfxSlider.value = MusicEffectsManager.SFXVolume;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetMusicVolume(float value)
    {
        MusicManager.MusicVolume = value;
    }

    void SetSFXVolume(float value)
    {
        MusicEffectsManager.SFXVolume = value;
    }
}