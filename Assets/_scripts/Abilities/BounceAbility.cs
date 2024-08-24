using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class BounceAbility : Ability
    {
        private readonly float _bounceMultiplier;
        private readonly float _previousMultiplier;

        // get stats object and extract values from it
        public BounceAbility(Stats stats) : base(stats.ActiveTime)
        {
            ThisStats = stats;
            _bounceMultiplier = ThisStats.BounceMultiplier;
            _previousMultiplier = ThisStats.JumpForceMultiplier;
            AbilityType = AbilityTypes.BounceType;
        }

        public override void Activate()
        {
            ThisStats.JumpForceMultiplier *= _bounceMultiplier;
        }

        public override void Deactivate()
        {
            ThisStats.JumpForceMultiplier = _previousMultiplier;
        }
        
        public override IEnumerator Perform()
        {
            yield return base.Perform();
        }
    }
}
