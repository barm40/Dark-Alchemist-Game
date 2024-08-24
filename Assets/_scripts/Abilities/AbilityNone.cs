using System.Collections;
using UnityEngine;

namespace Abilities
{
    /// <summary>
    /// None ability to avoid null pointer
    /// </summary>

    public class AbilityNone : Ability
    {
        public AbilityNone() : base(0f)
        {
            AbilityType = AbilityTypes.None;
            //Debug.Log($"This {AbilityType} is number: {abilityNumber}");
        }

        public override void Activate() {}

        public override void Deactivate() {}
    }
}
