using System.Collections;
using _managers;
using UnityEngine;

namespace Abilities
{
    public class BoostAbility : Ability
    {
        // speed multiplier
        private float SpeedMultiplier { get; set; } = 1.2f;
        // jump multiplier
        private float JumpMultiplier{ get; set; } = 1.2f;
        // light damage multiplier
        private float LightDamageMultiplier { get; set; } = 0.5f;
    
        // times from stats
        public BoostAbility()
        {
            AbilityType = AbilityTypes.BoostType;
        }

        protected override void Activate()
        {
            Stats.Instance.playerMoveStats.SetMoveMultiplier("Boost",SpeedMultiplier);
            Stats.Instance.playerJumpStats.NewJump(JumpMultiplier);
            Stats.Instance.playerLightDamageContainer.NewDamage(LightDamageMultiplier);
            PlayerController.IsBounce = true;
        }

        protected override void Deactivate()
        {
            Stats.Instance.playerMoveStats.ResetMultiplier("Boost");
            Stats.Instance.playerJumpStats.ResetJump();
            Stats.Instance.playerLightDamageContainer.ResetDamage();
            PlayerController.IsBounce = false;
        }

        public override IEnumerator Perform()
        {
            yield return base.Perform();
        }
    }
}
