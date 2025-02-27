using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bootstrapper
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Application.targetFrameRate = 60;
    }
}
