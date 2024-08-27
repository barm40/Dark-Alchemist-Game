using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Infra.Channels;
using Infra.Patterns;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _managers
{
    [RequireComponent(typeof(Stats))]
    public class AbilityController : Singleton<AbilityController>
    {
        [Header("Input Channel")]
        [SerializeField, Tooltip("Scriptable Object Channel to control Input")] 
        private PlayerInputChannel inputChannel;
        
        private float _abilityTime;
        private float _abilityCooldown;
    
        private bool _abilityIntent;

        private bool _chosenIntent;
        private short _chosenAbility;
        
        // private void OnEnable()
        // {
        //     inputChannel.SelectAbilityEvent += SelectAbility;
        //     inputChannel.PerformAbilityEvent += AbilityInput;
        // }
        //
        // private void OnDisable()
        // {
        //     inputChannel.SelectAbilityEvent -= SelectAbility;
        //     inputChannel.PerformAbilityEvent -= AbilityInput;
        // }
    
        public Dictionary<Ability.AbilityTypes, Ability> Abilities {  get; private set; } = new();
    
        [SerializeField] private ParticleSystem[] abilitiesVFX;
        // private bool _isCombo;

        protected override void Awake()
        {
            base.Awake();
            
            Abilities.Add(Ability.AbilityTypes.BounceType, new BounceAbility());
            Abilities.Add(Ability.AbilityTypes.BoostType, new BoostAbility());
            Abilities.Add(Ability.AbilityTypes.ImmuneType, new LightImmuneAbility());
        }

        public ParticleSystem GetAbilityVFX(Ability.AbilityTypes abilityTypes)
        {
            for (int i = 0; i < abilitiesVFX.Length; i++)
            {
                if (abilitiesVFX[i].name.Contains(abilityTypes.ToString().Substring(0,4)))
                {
                    return abilitiesVFX[i];
                }
            }
            return null;
        }
    }
}