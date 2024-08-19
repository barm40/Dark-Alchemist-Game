using UnityEngine;

namespace Abilities
{
    public abstract class Ability
    {
        protected Stats ThisStats;
    
        public enum AbilityTypes { None, BounceType, BoostType, ImmuneType }
        
        public AbilityTypes AbilityType { get; protected set; }
    
        public readonly float ActiveTime;
        public static int AmountOfAbilities;

        protected Ability(float activeTime)
        {
            ActiveTime = activeTime;
        }
        protected void SetAbilityNumber()
        {
            if (AbilityType != AbilityTypes.None)
            {
                AmountOfAbilities++;
            }
        }
        public abstract void Activate(GameObject parent);
        public abstract void Deactivate(GameObject parent);
    }
}
