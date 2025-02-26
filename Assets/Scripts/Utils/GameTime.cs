using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTime
{
    private static StopWatch stopWatch = new();

    public static void Start()
    {
        stopWatch.Start();
    }

    public static float Time() => stopWatch.TimerTime;

    public static float Stop()
    {
        return stopWatch.Stop(true);
    }
}
