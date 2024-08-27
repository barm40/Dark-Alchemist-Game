using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class LightImmuneAbility : Ability
    {
        // speed multiplier
        private float LightDamageMultiplier { get; set; } = 0f;
    
        public LightImmuneAbility()
        {
            AbilityType = AbilityTypes.ImmuneType;
        }

        protected override void Activate()
        {
            Stats.Instance.playerLightDamageContainer.NewDamage(LightDamageMultiplier);
        }

        protected override void Deactivate()
        {
            Stats.Instance.playerLightDamageContainer.NewDamage(LightDamageMultiplier);
        }
        
        public override IEnumerator Perform()
        {
            yield return base.Perform();
        }
    }
}
