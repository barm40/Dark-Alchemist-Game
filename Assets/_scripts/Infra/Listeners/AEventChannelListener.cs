using Infra.Channels;
using Infra.SOTypes;
using UnityEngine;
using UnityEngine.Events;

namespace Infra.Listeners
{
    public abstract class AEventChannelListener<TEvent> : MonoBehaviour
    {
        [Header("Listen to Event Channels")]
        [SerializeField] protected PlayerInputChannel inputChannel; 
        [Tooltip("Respond to receiving signal from event channel")]
        [SerializeField] protected UnityEvent<TEvent> response;
    
        public void OnEventRaised(TEvent evt)
        {
            response?.Invoke(evt);
        }
    }

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
}