using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private float perpectClearTime;
    [SerializeField] private float normalClearTime;

    public float PerpectClearTime => perpectClearTime;
    public float NormalClearTime => normalClearTime;

    void Start()
    {
        GameTime.Start();
    }

    public float WhatTime()
    {
        return GameTime.Stop();
    }
}

public class ClearEvent : IGameEvent
{
    public SceneLoader sceneLoader { get; set; }
}