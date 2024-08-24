using System;
using System.Collections;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability
    {
        protected Stats ThisStats;
        
        public enum AbilityTypes { None, BounceType, BoostType, ImmuneType }
        public AbilityTypes AbilityType { get; protected set; }
        
        public readonly float ActiveTime;

        public static event Action<AbilityTypes> AbilityEnded;

        protected Ability(float activeTime)
        {
            ActiveTime = activeTime;
        }

        public abstract void Activate();
        public abstract void Deactivate();

        public virtual IEnumerator Perform()
        {
            Debug.Log($"Ability {AbilityType} has started");
            Activate();
            yield return new WaitForSeconds(ActiveTime);
            Deactivate();
            AbilityEnded?.Invoke(AbilityType);
        }
    }
}

