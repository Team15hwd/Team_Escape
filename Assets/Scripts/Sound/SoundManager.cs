using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundEmitter emitterPrefab;

    [Header("Pool")]
    [SerializeField] private int initialPoolSize = 15;
    [SerializeField] private int maxPoolSize = 30;
    [SerializeField] private int maxSoundInstance = 5;

    private Dictionary<SoundData, int> soundInstances = new();
    private CustomObjectPool<SoundEmitter> emitterPool = new();

    public SoundBuilder Bulider() => new SoundBuilder(this);

    void Start()
    {
        emitterPool.Create(CreateObjects, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject,
            initialPoolSize, maxPoolSize);
    }

    public SoundEmitter Get()
    {
        return emitterPool.Get();
    }

    public bool CanPlaySound(SoundData data)
    {
        return !soundInstances.TryGetValue(data, out var count) || count < maxSoundInstance;
    }

    public void ReturnToPool(SoundEmitter emitter)
    {
        emitterPool.Release(emitter);
    }

    private SoundEmitter CreateObjects()
    {
        var go = Instantiate(emitterPrefab);
        go.gameObject.SetActive(false);
        go.transform.SetParent(this.transform);

        return go;
    }

    private void OnTakeFromPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(true);
    }

    public void OnReturnToPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(false);
    }

    public void OnDestroyPoolObject(SoundEmitter emitter)
    {
        Destroy(emitter.gameObject);
    }
}
