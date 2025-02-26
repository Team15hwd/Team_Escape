using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBuilder
{
    private SoundManager manager;

    private SoundData soundData;
    private Vector3 position = Vector2.zero;


    public SoundBuilder(SoundManager manager)
    {
        this.manager = manager;
    }

    public SoundBuilder WidthSoundData(SoundData data)
    {
        this.soundData = data;

        return this;
    }

    public SoundBuilder WidthSoundPosition(Vector3 position)
    {
        this.position = position;

        return this;
    }

    public SoundEmitter Play()
    {
        if (manager.CanPlaySound(soundData))
        {
            var emitter = manager.Get();

            if (emitter != null)
            {
                emitter.Initialize(soundData);
                emitter.transform.position = position;

                if (manager.CanPlaySound(soundData))
                {
                    emitter.Play();

                    return emitter;
                }
            }
        }

        return null;
    }
}