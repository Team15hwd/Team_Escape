using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class SoundEmitter : MonoBehaviour
{
    public SoundData SoundData { get; set; }

    private AudioSource source;
    private CancellationTokenSource cts;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Initialize(SoundData data)
    {
        SoundData = data;

        source.clip = data.clip;
        source.outputAudioMixerGroup = data.outputMixerGroup;
        source.mute = data.mute;
        source.loop = data.loop;
        source.playOnAwake = data.playOnAwake;
        source.pitch = data.pitch;
        source.volume = data.volume;
        source.panStereo = data.panStereo;
        source.spatialBlend= data.spatialBlend;
        source.reverbZoneMix= data.reverbZoneMix;
    }

    public void Play()
    {
        if (cts != null)
        {
            cts.Cancel();
        }
        source.Play();
        WaitForSoundToEnd().Forget();
    }

    private async UniTask WaitForSoundToEnd()
    {
        cts = new CancellationTokenSource();

        try
        {
            await UniTask.WaitUntil(() => !source.isPlaying);
            Stop();
        }
        catch (OperationCanceledException)
        {

        }
    }

    public void Stop()
    {
        SoundManager.Instance.ReturnToPool(this);
    }

    void OnDisable()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }
}
