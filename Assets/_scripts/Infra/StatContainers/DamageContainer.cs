using System;
using Infra.SOTypes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infra.StatContainers
{
    [CreateAssetMenu(fileName = "New Damage Container", menuName = "Stats/Damage")]
    public class DamageContainer : AStatContainer<Damage>
    {
        public float CurrentDamage { get; private set; }
        
        [SerializeField] public Damage damage = new (DamageType.Light, 20f, 1f);
        
        private float _originalDamageMulti;
        
        private void OnEnable()
        {
            _originalDamageMulti = damage.damageMultiplier;
            ResetDamage();
        }
        
        public void NewDamage(float multiplier)
        {
            CurrentDamage = damage.baseDamage * multiplier;
        }
    
        public void ResetDamage()
        {
            NewDamage(_originalDamageMulti);
        }
    }

    [Serializable]
    public struct Damage : IStats
    {
        public DamageType type;
        public float baseDamage;
        public float damageMultiplier;
    
        public Damage (DamageType type, float baseDamage, float damageMultiplier)
        {
            this.baseDamage = baseDamage;
            this.type = type;
            this.damageMultiplier = damageMultiplier;
        }
    }

    public enum DamageType
    {
        Light,
        Fire
    }
}