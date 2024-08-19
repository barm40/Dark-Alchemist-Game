using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _managers
{
    [RequireComponent(typeof(Stats))]
    public class AbilityController : MonoBehaviour
    {
        private Stats _stats;
        private InventoryManager _inventoryManager;

        private float _abilityTime;
        private float _abilityCooldown;
    
        private bool _abilityIntent;

        private bool _chosenIntent;
        private short _chosenAbility;
    
        private AbilityNone _defaultAbility;
        private AbilityState _abilityState = AbilityState.Ready;
    
        // [SerializeField] public Ability CurrentAbility { get; set; }

        private short AbilityChosenCount { get; set; }
        private readonly Dictionary<Ability.AbilityTypes, bool> _chosenAbilityList = new()
        {
            {Ability.AbilityTypes.BounceType, false },
            {Ability.AbilityTypes.BoostType, false },
            {Ability.AbilityTypes.ImmuneType, false }
        };
    
        public Dictionary<Ability.AbilityTypes, Ability> Abilities {  get; private set; } = new();
    
        [SerializeField] private ParticleSystem[] abilitiesVFX;
        // private bool _isCombo;

        private void Awake()
        {
            _stats = GetComponent<Stats>();
            _inventoryManager = GameObject.FindGameObjectWithTag("inventory").GetComponent<InventoryManager>();
            _defaultAbility = new AbilityNone();
            // CurrentAbility = _defaultAbility;

            Abilities.Add(Ability.AbilityTypes.BounceType, new BounceAbility(_stats));
            Abilities.Add(Ability.AbilityTypes.BoostType, new BoostAbility(_stats));
            Abilities.Add(Ability.AbilityTypes.ImmuneType, new LightImmuneAbility(_stats));
        }

        private void Update()
        {
            if (_abilityState != AbilityState.Active)
            {
                ChooseAbility(_chosenAbility);
            }
            DoAbility();
        }

        private void DoAbility()
        {
            switch (_abilityState)
            {
                case AbilityState.Ready:
                {
                    if (_abilityIntent && AbilityChosenCount > 0)
                    {
                        foreach (var ability in Abilities.Values.Where(ability => _chosenAbilityList[ability.AbilityType]))
                        {
                            ability.Activate(gameObject);
                            _abilityState = AbilityState.Active;
                            _abilityTime =  ability.ActiveTime;
                            Debug.Log($"the ability {ability.AbilityType} is chosen");
                            _inventoryManager.UseAbilityItem((int)ability.AbilityType - 1);
                            if (ability.AbilityType is Ability.AbilityTypes.BoostType)
                                PlayerController.IsBounce = true;
                            GetAbilityVFX(ability.AbilityType)?.Play();
                        }
                        // _isCombo = false;
                        
                        // CurrentAbility.Activate(gameObject);
                        // _abilityState = AbilityState.Active;
                        // _abilityTime = CurrentAbility.ActiveTime;
                        // _inventoryManager.UseAbilityItem(Current(int)ability.AbilityType - 1);
                        // if(CurrentAbility.AbilityType == Ability.AbilityTypes.BounceType)
                        //     PlayerController.IsBounce = true;
                        // GetAbilityVFX(CurrentAbility.AbilityType)?.Play();
                    }
                }
                    break;
                case AbilityState.Active:
                {
                    if (_abilityTime <= 0)
                    {
                        foreach (var ability in Abilities.Values.Where(ability => _chosenAbilityList[ability.AbilityType]))
                        {
                            ability.Deactivate(gameObject);
                            _abilityState = AbilityState.Ready;
                            if (ability.AbilityType is Ability.AbilityTypes.BoostType)
                                PlayerController.IsBounce = false;
                            GetAbilityVFX(ability.AbilityType)?.Stop();
                        }
                        
                        // CurrentAbility.Deactivate(gameObject);
                        // _abilityState = AbilityState.Ready;
                        // if(CurrentAbility.AbilityType == Ability.AbilityTypes.BounceType)
                        //     PlayerController.IsBounce = false;
                        // GetAbilityVFX(CurrentAbility.AbilityType)?.Stop();
                        
                        ClearAbility();
                    }
                    else
                    {
                        _abilityTime -= Time.deltaTime;
                    }
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
        public void AbilityInput(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                _abilityIntent = true;
            }
            else if (context.canceled)
            {
                _abilityIntent = false;
            }
        }

        public void SelectAbility(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                _chosenAbility = (short)context.ReadValue<float>();
                _chosenIntent = true;
            }
        }

        private void ChooseAbility(short chosenAbility)
        {
            if (chosenAbility == 0) return;

            if (_chosenIntent && _inventoryManager.IsTheItemInInventory((int)Abilities[(Ability.AbilityTypes)chosenAbility].AbilityType - 1))
            {
                SetCurrentAbility((Ability.AbilityTypes)chosenAbility);
                _chosenIntent = false;
            }
        }

        private void SetCurrentAbility(Ability.AbilityTypes abilityType)
        {
            // if in combo mode and user diselect ability
            // if the user
            if (AbilityChosenCount == 0)
            {
                _chosenAbilityList[abilityType] = true;

                foreach (var ability in Abilities.Values.Where(ability => abilityType == ability.AbilityType))
                {
                    // CurrentAbility = CurrentAbility is null or AbilityNone ? ability : _defaultAbility;
                    AbilityChosenCount++;
                    break;
                }
            }
            else if (_chosenAbilityList[abilityType])
            {
                _chosenAbilityList[abilityType] = false;
                AbilityChosenCount--;
                // foreach (var ability in AbilitiesList.Where(ability => _chosenAbilityList[ability.AbilityType]))
                // {
                //     CurrentAbility = ability;
                // }
            }
            else if (AbilityChosenCount is > 0 and < 2)
            {
                _chosenAbilityList[abilityType] = true;
                AbilityChosenCount++;
            }
        }

        /// <summary>
        /// This function for use when have combo mode element,
        /// actually when we will have more abilities we need to use it to set the combo ability and from here call to 'Do ability' 
        /// </summary>
        private void UseAbility()
        {
            foreach (var ability in Abilities.Values.Where(ability => _chosenAbilityList[ability.AbilityType]))
            {
                ability.Activate(gameObject);
                _abilityState = AbilityState.Active;
                _abilityTime = ability.ActiveTime > _abilityTime? ability.ActiveTime : _abilityTime;
                Debug.Log($"the ability {ability.AbilityType} is choosed");
                _inventoryManager.UseAbilityItem((int)ability.AbilityType - 1);
                if (ability.AbilityType is Ability.AbilityTypes.BoostType)
                    PlayerController.IsBounce = true;
                GetAbilityVFX(ability.AbilityType)?.Play();
            }
            // _isCombo = false;
            ClearAbility();
            // To Do - remove it after have the combo abilities
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

        private void ClearAbility()
        {
            AbilityChosenCount = 0;

            foreach (var ability in Abilities.Values)
            {
                _chosenAbilityList[ability.AbilityType] = false;
            }
        }

        private enum AbilityState
        {
            Ready,
            Active
        }
    }
}