using UnityEngine;

namespace Infra.StatContainers
{
    public class PlayerStatManager : MonoBehaviour
    {
        [SerializeField] private MoveStatContainer moveStats;
        [SerializeField] private DashStatContainer dashStats;
    
        // player related fields
        // move
        private float baseMoveSpeed;
        private float maxMoveSpeed;
        private float MoveSpeedMultiplier;
    
        public float CurrentMoveSpeed { get; private set; }
    
        // jump
        private float baseJumpForce;
        private float maxJumpForce;
        private float JumpForceMultiplier;
    
        public float Hp { get; set; }
    
        // light damage related
        public float baseLightDamage;
        public float shieldLightDamage;

        public float CurrentLightDamage { get; set; }
    
        // general ability related stats 
        public float ActiveTime { get; private set; }
        public float CooldownTime { get; private set; }
    
        public float DashActive { get; private set; }
        public float DashCooldown { get; private set; }
        public float DashMultiplier { get; private set; }
    
        // boost specific stats
        private float BoostMultiplier;
        private float BoostNegativeMultiplier;
    
        // bounce specific stats
        private float BounceMultiplier;


        private void Init()
        {
        
        }
    
        private void FixedUpdate()
        {
            CurrentMoveSpeed = 
                baseMoveSpeed * MoveSpeedMultiplier * Time.deltaTime;
        }
    }
}
