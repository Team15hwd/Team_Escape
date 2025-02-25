using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundEmitter emitterPrefab;

    private Dictionary<SoundData, SoundEmitter> sounds = new();
    private CustomObjectPool<SoundEmitter> emitterPool = new();


    void Awake()
    {
        emitterPool.Create(CreateObjects, TakeFromPool, ReturnToPool, DeletePoolObject,
            10, 20);
    }

    public void PlaySound(SoundData data)
    {

    }

    private SoundEmitter CreateObjects()
    {
        var go = Instantiate(emitterPrefab);
        go.gameObject.SetActive(false);
        go.transform.SetParent(this.transform);

        return go;
    }

    private void TakeFromPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(true);
    }

    public void ReturnToPool(SoundEmitter emitter)
    {
        emitter.gameObject.SetActive(false);
    }

    public void DeletePoolObject(SoundEmitter emitter)
    {
        Destroy(emitter.gameObject);
    }
}
