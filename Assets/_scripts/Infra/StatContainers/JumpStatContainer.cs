using System;
using Infra.SOTypes;
using UnityEngine;

namespace Infra.StatContainers
{
    [CreateAssetMenu(fileName = "New Jump Stats", menuName = "Stats/Jump Stats")]
    public class JumpStatContainer : AStatContainer<JumpStats>
    {
        public float CurrentJumpForce { get; private set; }

        private float _originalJumpMulti;

        [Header("Jump Stat Values")]
        [SerializeField] 
        public JumpStats jumpStats = new (150f, 1f, .2f, .4f);

        private void OnEnable()
        {
            _originalJumpMulti = jumpStats.jumpMultiplier;
            ResetJump();
        }

        public void NewJump(float multiplier)
        {
            CurrentJumpForce = 
                jumpStats.baseJumpForce * multiplier * Time.deltaTime;
        }

        public void ResetJump()
        { 
            NewJump(_originalJumpMulti);
        }
    }

    [Serializable]
    public struct JumpStats : IStats
    {
        public float baseJumpForce;
        public float jumpMultiplier;
        public float coyoteTime;
        public float jumpBufferTime;

        public JumpStats(float jumpForce, float jumpMultiplier, float coyoteTime, float jumpBufferTime)
        {
            baseJumpForce = jumpForce;
            this.jumpMultiplier = jumpMultiplier;
            this.coyoteTime = coyoteTime;
            this.jumpBufferTime = jumpBufferTime;
        }
    }
}