using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected bool dontDestroy = false;

    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();
            }

            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).Name + "Created";
                instance = go.AddComponent<T>();
            }

            return instance;
        }
    }

    void Awake()
    {
        if (dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected Singleton() { }
}
