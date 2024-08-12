using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class AEventChannelListener<TChannel, TEvent> : MonoBehaviour where TChannel : AEventChannel<TEvent>
{
    [Header("Listen to Event Channels")]
    [SerializeField] protected TChannel channelType;
    [Tooltip("Respond to receiving signal from event channel")]
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
