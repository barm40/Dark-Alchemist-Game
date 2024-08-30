using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class BounceAbility : Ability
    {
        // jump multiplier
        private float JumpMultiplier { get; set; } = 1.5f;

        // get stats object and extract values from it
        public BounceAbility()
        {
            AbilityType = AbilityTypes.BounceType;
        }

        protected override void Activate()
        {
            Stats.Instance.playerJumpStats.NewJump(JumpMultiplier);
        }

        protected override void Deactivate()
        {
            Stats.Instance.playerJumpStats.NewJump(JumpMultiplier);
        }
        
        public override IEnumerator Perform()
        {
            yield return base.Perform();
        }
    }
}
