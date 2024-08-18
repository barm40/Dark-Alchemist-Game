using System;
using Infra.SOTypes;
using UnityEngine;

namespace Infra.StatContainers
{
    [CreateAssetMenu(fileName = "New Damage Container", menuName = "Stats/Damage")]
    public class DamageContainer : AStatContainer<Damage>
    {
        [SerializeField] public Damage damage = new Damage(20f, DamageType.Light);
    }

    [Serializable]
    public struct Damage : IStats
    {
        public DamageType type;
        public float amount;
    
        public Damage (float amount, DamageType type)
        {
            this.amount = amount;
            this.type = type;
        }
    }

    public enum DamageType
    {
        Light,
        Fire
    }
}