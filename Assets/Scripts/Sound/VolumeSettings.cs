using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider audioSlider;

    private void Start()
    {
        Data_Manipulator.Instance.LoadSettings();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20f);
    }
    public void SetSfxVolume()
    {
        float volume = audioSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20f);
    }
    public void LoadMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20f);
        musicSlider.value = volume;
    }
    public void LoadSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20f);
        audioSlider.value = volume;
    }
}
