using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomObjectPool<T> where T : Component
{
    private List<T> activateObjects = new();

    private int capacity;
    private int addtiveCount;
    private int objectCount;

    private Func<T> OnCreated;
    private event Action<T> OnGet = delegate { };
    private event Action<T> OnRelease = delegate { };
    private event Action<T> OnDelete = delegate { };

    public void Create(Func<T> createFunc, Action<T> actionOnGet, Action<T> actionOnRelease, Action<T> actionOnDelete,
        int initalSize, int capacity, int addtive = 1)
    {
        this.capacity = capacity;
        this.objectCount = initalSize;
        this.addtiveCount = addtive;

        OnCreated += createFunc;
        OnGet += actionOnGet;
        OnRelease += actionOnRelease;
        OnDelete += actionOnDelete;

        for (int i = 0; i < initalSize; i++)
        {
            activateObjects.Add(OnCreated?.Invoke());
        }

        OnGet += (ob) => activateObjects.Remove(ob);
    }

    public T Get()
    {
        T obj;

        if (activateObjects.Count > 0)
        {
            obj = activateObjects[0];
            OnGet?.Invoke(obj);

            return obj;
        }
        else
        {
            var count = capacity - objectCount > 0 ? addtiveCount : 0;

            if (count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    activateObjects.Add(OnCreated?.Invoke());
                    objectCount++;
                }
            }
        }

        obj = activateObjects[0];
        OnGet?.Invoke(obj);

        return obj;
    }

    public void Release(T obj)
    {
        activateObjects.Add(obj);
        OnRelease?.Invoke(obj);
    }

    public void Delete(T obj)
    {
        activateObjects.Remove(obj);
        OnDelete?.Invoke(obj);
    }

}
