using System.Collections.Generic;
using Infra.Channels;
using Infra.StatContainers;
using UnityEngine;

namespace _managers
{
    public class PlayerHealthController : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField, Tooltip("Scriptable Object Channel player HP")] 
        private PlayerHealthChannel healthChannel;
        [SerializeField, Tooltip("Scriptable Object Damage Types and Values")] 
        private DamageContainer[] damageContainers;
    
        // Dictionary for each damage container's type and value
        private Dictionary<DamageType, float> _damageStats;

        private void OnEnable()
        {
            PlayerInLighDetect.UserInTheLightDelegate += ReduceHealth;
        }

        private void OnDisable()
        {
            PlayerInLighDetect.UserInTheLightDelegate -= ReduceHealth;
        }

        private void Awake()
        {
            // Populate dictionary with each damage container's type and value
            _damageStats = new Dictionary<DamageType, float>();
            foreach (var damageContainer in damageContainers)
            {
                _damageStats.Add(damageContainer.damage.type, damageContainer.damage.amount);
            }
        }

        private void Start()
        {
            healthChannel.QueryHealth();
        }

        private void ReduceHealth(DamageType damageType)
        {
            healthChannel.ChangeHealth(_damageStats[damageType]);

            if (!VFXManager.Instance.IsHitEffectActive)
            {
                StartCoroutine(VFXManager.Instance.GetHitLightEffect());
            }
        }
    }
}
