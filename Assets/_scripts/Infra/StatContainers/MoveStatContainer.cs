using System;
using System.Collections.Generic;
using Infra.SOTypes;
using UnityEngine;

namespace Infra.StatContainers
{
    [CreateAssetMenu(fileName = "New Movement Stats", menuName = "Stats/Movement")]
    public class MoveStatContainer : AStatContainer<MoveStats>
    {
        public float CurrentMoveSpeed { get; private set; }
        
        [SerializeField] 
        public MoveStats moveStats = new (350f, 10f, 1f);

        private readonly Dictionary<string, float> _multipliers = new();

        private void OnEnable()
        {
            InitSpeed();
        }

        private void InitSpeed()
        {
            CurrentMoveSpeed = moveStats.baseMoveSpeed * moveStats.moveSpeedMultiplier;
        }

        public void SetMoveMultiplier(string key, float value)
        {
            _multipliers[key] = value;
            CurrentMoveSpeed = CalcTargetSpeed();
        }
    
        public void ResetMultiplier(string key)
        {
            _multipliers[key] = 1;
            var targetSpeed = CalcTargetSpeed();
            
            CurrentMoveSpeed =
                Mathf.Lerp(CurrentMoveSpeed, targetSpeed, 5f * Time.deltaTime);
        }

        private float CalcTargetSpeed()
        {
            var targetSpeed = moveStats.baseMoveSpeed;
            foreach (var multiplier in _multipliers)
            {
                if (multiplier.Value <= 0)
                    continue;
                targetSpeed *= multiplier.Value;
            }

            return targetSpeed;
        }
    }

    [Serializable]
    public struct MoveStats : IStats
    {
        public float baseMoveSpeed;
        public float moveSpeedMultiplier;

        public MoveStats(float baseSpeed, float maxSpeed, float multi)
        {
            baseMoveSpeed = baseSpeed;
            moveSpeedMultiplier = multi;
        }
    }
}