using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public abstract class Timer
{
    protected float time;
    public float TimerTime => time;
    public event Action OnTimerStart = delegate { };
    public event Action OnTimerUpdate = delegate { };
    public event Action OnTimerStop = delegate { };

    public abstract void Start(bool ignoreTimeScale);

    protected void OnTimerStartInvoke()
    {
        OnTimerStart?.Invoke();
    }

    protected void OnTimerUpdateInvoke()
    {
        OnTimerUpdate?.Invoke();
    }

    protected void OnTimerStopInvoke()
    {
        OnTimerStop?.Invoke();
    }
}

public class CountdownTimer : Timer
{
    protected float internalTime;

    public CountdownTimer(float time)
    {
        this.time = time;
        internalTime = time;
    }

    public CountdownTimer()
    {

    }

    public override void Start(bool ignoreTimeScale = false)
    {
        StartAsync(ignoreTimeScale).Forget();
    }

    protected async UniTask StartAsync(bool ignoreTimeScale)
    {
        OnTimerStartInvoke();

        while (time > 0f)
        {
            await UniTask.NextFrame(PlayerLoopTiming.Update);

            time -= ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            OnTimerUpdateInvoke();
        }

        OnTimerStopInvoke();
    }

    public void Reset(float newTime, bool ignoreTimeScale = false)
    {
        time = newTime;
    }

    public void Reset(bool ignoreTimeScale = false)
    {
        time = internalTime;
    }
}

public class StopWatch : Timer
{
    private CancellationTokenSource cts;
    private bool isResume = true;

    public override void Start(bool ignoreTimeScale = false)
    {
        End();

        time = 0f;
        isResume = true;
        StartAsync(ignoreTimeScale).Forget();
    }

    protected async UniTask StartAsync(bool ignoreTimeScale)
    {
        cts = new();
        
        OnTimerStartInvoke();

        while (cts != null && !cts.IsCancellationRequested)
        {
            await UniTask.NextFrame(PlayerLoopTiming.Update);

            if (isResume)
            {
                time += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
                OnTimerUpdateInvoke();
            }
        }

        OnTimerStopInvoke();
    }

    public float Stop(bool isEnd = true)
    {
        isResume = false;

        if (isEnd)
        {
            End();
        }

        return time;
    }
    
    public void End()
    {
        cts?.Cancel();
    }

    public void Reset()
    {
        time = 0f;
    }

    public void Resume()
    {
        isResume = true;
    }
}