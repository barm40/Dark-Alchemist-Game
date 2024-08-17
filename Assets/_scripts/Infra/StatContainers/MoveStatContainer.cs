using System;
using Infra.SOTypes;
using UnityEngine;

namespace Infra.StatContainers
{
    [CreateAssetMenu(fileName = "New Movement Stats", menuName = "Stats/Movement")]
    public class MoveStatContainer : AStatContainer<MoveStats>
    {
        public float CurrentMoveSpeed { get; private set; }

        private float _originalMulti;

        [SerializeField] 
        public MoveStats moveStats = new (350f, 10f, 1f);

        private void OnEnable()
        {
            _originalMulti = moveStats.moveSpeedMultiplier;
            NewSpeed(moveStats.moveSpeedMultiplier);
        }

        public void NewSpeed(float multiplier)
        {
            CurrentMoveSpeed = moveStats.baseMoveSpeed * multiplier;
        }
    
        public void ResetSpeed()
        {
            CurrentMoveSpeed = 
                Mathf.Lerp(CurrentMoveSpeed, moveStats.baseMoveSpeed * _originalMulti, 5f * Time.deltaTime);
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