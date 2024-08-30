using System;
using System.Collections;
using _managers;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability
    {
        public enum AbilityTypes { None, BounceType, BoostType, ImmuneType }

        protected AbilityTypes AbilityType { get; set; }

        private readonly float _activeTime = Stats.Instance.abilityActiveTime;

        public static event Action<AbilityTypes> AbilityEnded;

        protected abstract void Activate();
        protected abstract void Deactivate();

        public virtual IEnumerator Perform()
        {
            Debug.Log($"Ability {AbilityType} has started");
            
            Activate();
            AbilityController.Instance.GetAbilityVFX(AbilityType).Play();
            
            yield return new WaitForSeconds(_activeTime);
            
            Deactivate();
            AbilityController.Instance.GetAbilityVFX(AbilityType).Stop();
            AbilityEnded?.Invoke(AbilityType);
        }
    }
}

