using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        mixer.GetFloat(SoundType.BGM.ToString(), out var bgmVolume);
        mixer.GetFloat(SoundType.SFX.ToString(), out var sfxVolume);

        bgmSlider.value = Mathf.Pow(10, bgmVolume * 0.05f);
        sfxSlider.value = Mathf.Pow(10, sfxVolume * 0.05f);

        bgmSlider.onValueChanged.AddListener((val) =>
        {
            SetBGMVolume(val);
        });

        sfxSlider.onValueChanged.AddListener((val) =>
        {
            SetSFXVolume(val);
        });
    }

    public void SetBGMVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20f;

        mixer.SetFloat("BGM", volume);
    }

    public void SetSFXVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20f;

        mixer.SetFloat(SoundType.SFX.ToString(), volume);
    }
}

public enum SoundType
{
    Master,
    BGM,
    SFX
}

