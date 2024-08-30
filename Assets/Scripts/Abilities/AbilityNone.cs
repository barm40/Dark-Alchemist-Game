using System.Collections;
using UnityEngine;

namespace Abilities
{
    /// <summary>
    /// None ability to avoid null pointer
    /// </summary>

    public class AbilityNone : Ability
    {
        public AbilityNone() : base()
        {
            AbilityType = AbilityTypes.None;
        }

        protected override void Activate() {}

        protected override void Deactivate() {}
    }
}
