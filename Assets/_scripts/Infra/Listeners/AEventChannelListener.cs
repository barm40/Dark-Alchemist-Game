using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class AEventChannelListener<TChannel, TEvent> : MonoBehaviour where TChannel : AEventChannel<TEvent>
{
    [SerializeField] protected TChannel channelType;
    [SerializeField] protected UnityEvent<TEvent> response;

    protected virtual void OnEnable()
    {
        if (channelType != null) channelType.OnEventRaised += OnEventRaised;
    }

    protected virtual void OnDisable()
    {
        if (channelType != null) channelType.OnEventRaised -= OnEventRaised;
    }
    
    public void OnEventRaised(TEvent evt)
    {
        response?.Invoke(evt);
    }
}
