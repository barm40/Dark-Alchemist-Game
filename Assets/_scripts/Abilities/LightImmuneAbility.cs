using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class LightImmuneAbility : Ability
    {
        private readonly float _prevLightDamage;
        private readonly float _shieldLightDamage;
    
        public LightImmuneAbility(Stats stats) : base(stats.ActiveTime)
        {
            ThisStats = stats;
            _prevLightDamage = ThisStats.lightDamage;
            _shieldLightDamage = ThisStats.shieldLightDamage;
            AbilityType = AbilityTypes.ImmuneType;
        }

        public override void Activate()
        {
            ThisStats.lightDamage = _shieldLightDamage;
        }

        public override void Deactivate()
        {
            ThisStats.lightDamage = _prevLightDamage;
        }
        
        public override IEnumerator Perform()
        {
            yield return base.Perform();
        }
    }
}
