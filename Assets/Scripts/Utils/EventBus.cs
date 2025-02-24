using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static Dictionary<Type, Delegate> assignedActions = new();

    public static void Call<T>(T eventData) where T : IGameEvent
    {
        Type type = eventData.GetType();

        if (assignedActions.TryGetValue(eventData.GetType(), out var action))
        {
            (action as Action<T>)?.Invoke(eventData);
        }
    }

    public static void Subscribe<T>(Action<T> action) where T : IGameEvent
    {
        Type type = typeof(T);

        if (assignedActions.ContainsKey(type))
        {
            assignedActions[type] = Delegate.Combine(assignedActions[type], action);
        }
        else
        {
            assignedActions[type] = action;
        }
    }

    public static void Unsubscribe<T>(Action<T> action) where T : IGameEvent
    {
        Type type = typeof(T);

        if (assignedActions.ContainsKey(type))
        {
            assignedActions[type] = Delegate.Remove(assignedActions[type], action);
        }
    }
}

public interface IGameEvent
{

}
