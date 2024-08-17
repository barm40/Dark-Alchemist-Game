using UnityEngine;

namespace Abilities
{
    public abstract class Ability
    {
        protected Stats ThisStats;
    
        public enum AbilityTypes { None, BounceType, BoostType, ImmuneType }
        public AbilityTypes AbilityType { get; protected set; }
    
        public readonly float ActiveTime;
        private static int _amountOfAbilities;
        public int AbilityNumber;

        protected Ability(float activeTime)
        {
            ActiveTime = activeTime;
        }
        protected void SetAbilityNumber()
        {
            if (AbilityType != AbilityTypes.None)
            {
                AbilityNumber = _amountOfAbilities;
                _amountOfAbilities++;
            }
            Debug.Log($"This {AbilityType} is number: {AbilityNumber}");
        }
        public abstract void Activate(GameObject parent);
        public abstract void Deactivate(GameObject parent);
    }
}
