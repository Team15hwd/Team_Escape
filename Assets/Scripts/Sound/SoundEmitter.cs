using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SoundEmitter : MonoBehaviour
{
    public SoundData SoundData { get; set; }

    private AudioSource source;

    private async UniTask WaitForSoundToEnd()
    {
        await UniTask.WaitUntil(() => !source.isPlaying);
        Stop();
    }

    public void Stop()
    {
        SoundManager.Instance.ReturnToPool(this);
    }
}
