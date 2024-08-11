using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<EventID, Action<object>> listeners = new Dictionary<EventID, Action<object>>();
    #region Register, Unregister, Broadcast
    public void Register(EventID id, Action<object> action)
    {
        if (!listeners.ContainsKey(id))
        {
            listeners[id] = action;
        }
        else
        {
            listeners[id] += action;
        }
    }

    public void Unregister(EventID id, Action<object> action)
    {
        if (listeners.ContainsKey(id))
        {
            listeners[id] -= action;
        }
    }
    public void Broadcast(EventID id, object data)
    {
        if (listeners.ContainsKey(id))
        {
            listeners[id]?.Invoke(data);
        }
        
    }

    #endregion
}

