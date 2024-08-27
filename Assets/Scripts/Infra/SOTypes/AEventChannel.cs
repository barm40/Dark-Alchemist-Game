using UnityEngine;
using UnityEngine.Events;

namespace Infra.SOTypes
{
    public abstract class AEventChannel<TEvent> : ScriptableObject
    {
        // Action to perform
        [Tooltip("The action that will be performed. Event type")]
        public UnityAction<TEvent> OnEventRaised;

        protected void RaiseEvent(TEvent param) => OnEventRaised?.Invoke(param);
    }

    public interface IGenericEvent
    {
    }
}