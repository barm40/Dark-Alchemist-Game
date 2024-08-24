using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class BoostAbility : Ability
    {
        private readonly float _previousSpeedMultiplier;
        private readonly float _previousJumpMultiplier;
        private readonly float _previousLightMultiplier;
    
        // times from stats
        public BoostAbility(Stats stats) : base(stats.ActiveTime)
        {
            ThisStats = stats;
            AbilityType = AbilityTypes.BoostType;
        
            _previousSpeedMultiplier = ThisStats.MoveSpeedMultiplier;
            _previousJumpMultiplier = ThisStats.JumpForceMultiplier;
            _previousLightMultiplier = ThisStats.lightDamage;
        }

        public override void Activate()
        {
            ThisStats.MoveSpeedMultiplier *= ThisStats.BoostMultiplier;
            ThisStats.JumpForceMultiplier *= ThisStats.BoostMultiplier;
            ThisStats.lightDamage *= ThisStats.BoostNegativeMultiplier;
        }

        public override void Deactivate()
        {
            ThisStats.MoveSpeedMultiplier = _previousSpeedMultiplier;
            ThisStats.JumpForceMultiplier = _previousJumpMultiplier;
            ThisStats.lightDamage = _previousLightMultiplier;
        }

        public override IEnumerator Perform()
        {
            yield return base.Perform();
        }
    }
}
