using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundData
{
    public AudioClip clip;
    public AudioMixerGroup outputMixerGroup;
    public bool mute = false;
    public bool loop = false;
    public bool playOnAwake = false;
    [Range(0f, 1f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(-1f, 1f)]
    public float panStereo;
    [Range(0f, 1f)]
    public float spatialBlend = 0f;
    [Range(0f, 1.1f)]
    public float reverbZoneMix = 1f;
}
