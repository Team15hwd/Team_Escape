using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] private SoundData soundData;

    private SoundEmitter emitter;
    public void Play()
    {
        emitter = SoundManager.Instance.Bulider().WidthSoundData(soundData).Play();
    }

    public void Stop()
    {
        emitter?.Stop();
    }
}
