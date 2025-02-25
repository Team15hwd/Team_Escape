using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bootstrapper
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Debug.Log("Loaded");
        Application.targetFrameRate = 60;
    }
}
