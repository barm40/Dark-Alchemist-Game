using System;
using Infra.SOTypes;
using UnityEngine;

namespace Infra.StatContainers
{
    /// <summary>
    /// This is a Scriptable Object that contains stats for dash action
    /// It creates a struct of base values for dash
    /// and can be triggered to raise a DashEvent that passes the Dash Speed Multiplier.
    /// </summary>
    [CreateAssetMenu(fileName = "New Dash Stats", menuName = "Stats/Dash")]
    public class DashStatContainer : AStatContainer<DashStats>
    {
        [SerializeField] 
        public DashStats dashStats = new (.3f, .7f, 3f);
    }

    [Serializable]
    public struct DashStats : IStats
    {
        public float dashActive;
        public float dashCooldown;
        public float dashMultiplier;

        public DashStats(float active, float cooldown, float multi)
        {
            dashActive = active;
            dashCooldown = cooldown;
            dashMultiplier = multi;
        }
    }
}