using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomObjectPool<T> where T : Component
{
    private List<T> objects = new();

    private int capacity;
    private int addtiveCount;

    private event Action<T> OnGet = delegate { };
    private event Action<T> OnRelease = delegate { };
    private event Action<T> OnDelete = delegate { };

    public void Create(Func<T> createFunc, Action<T> actionOnGet, Action<T> actionOnRelease, Action<T> actionOnDelete,
        int initalSize, int capacity, int addtive = 1)
    {
        this.capacity = capacity;
        this.addtiveCount = addtive;

        actionOnRelease += Release;
        actionOnDelete += Delete;

        for (int i = 0; i < initalSize; i++)
        {
            var go = createFunc?.Invoke();

            objects.Add(go);
        }
    }

    public T Get()
    {
        if (objects.Count != 0)
        {
            return objects[0];
        }
        else
        {

        }
        return objects[0];
    }

    public void Release(T obj)
    {
        objects.Add(obj);
    }

    public void Delete(T obj)
    {
        objects.Remove(obj);
    }

}
