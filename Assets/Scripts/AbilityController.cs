using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Stats))]

public class AbilityController : MonoBehaviour
{
    private Stats _stats;
    private InvantoryManager invantoryManager;

    private float _abilityTime;
    private float _abilityCooldown;
    
    private AbilityState _abilityState = AbilityState.Ready;
    
    [SerializeField] public Ability CurrentAbility { get; set; }
    [SerializeField] private KeyCode abilityKey = KeyCode.LeftShift;
    
    public bool _isAbilityChoosed { private get; set; }
    private Dictionary<Ability.AbilityTypes, bool> abilityChoosenList = new Dictionary<Ability.AbilityTypes, bool>()
    {
        {Ability.AbilityTypes.DashType, false },
        {Ability.AbilityTypes.BoostType, false },
        {Ability.AbilityTypes.ImmuneType, false }
    };
    public List<Ability> abilitiesList {  get; private set; } = new List<Ability>();

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        invantoryManager = FindObjectOfType<InvantoryManager>();
        //CurrentAbility = new DashAbility(_stats);
        //_isAbilityChoosed = true;
        abilitiesList.Add(new DashAbility(_stats));
        abilitiesList.Add(new BoostAbility(_stats));
        abilitiesList.Add(new LightImmuneAbility(_stats));
    }

    private void Update()
    {
        if (_abilityState != AbilityState.Active)
        {
            ChooseAbility();
        }
        DoAbility();
    }

    private void DoAbility()
    {
        switch (_abilityState)
        {
            case AbilityState.Ready:
                {
                    if (Input.GetKeyDown(abilityKey) && _isAbilityChoosed)
                    {
                        CurrentAbility.Activate(gameObject);
                        _abilityState = AbilityState.Active;
                        _abilityTime = CurrentAbility.ActiveTime;
                        invantoryManager.useAbilityItem(CurrentAbility.abilityNumber);
                    }
                }
                break;
            case AbilityState.Active:
                {
                    if (_abilityTime <= 0)
                    {
                        CurrentAbility.Deactivate(gameObject);
                        _abilityState = AbilityState.Ready;
                        //_abilityState = AbilityState.Cooldown;
                        //_abilityCooldown = CurrentAbility.CooldownTime;
                        ClearAbilityAfterUsed();
                    }
                    else
                    {
                        _abilityTime -= Time.deltaTime;
                        // Dash only works while key pressed
                        if (CurrentAbility.AbilityType == Ability.AbilityTypes.DashType && Input.GetKeyUp(abilityKey))
                        {
                            CurrentAbility.Deactivate(gameObject);
                            _abilityState = AbilityState.Ready;
                            //_abilityState = AbilityState.Cooldown;
                            //_abilityCooldown = CurrentAbility.CooldownTime;
                        }
                    }
                }
                break;
            //case AbilityState.Cooldown:
            //    {
            //        if (_abilityCooldown > 0)
            //            _abilityCooldown -= Time.deltaTime;
            //        else
            //        {
            //            _abilityState = AbilityState.Ready;
            //            ClearAbilityAfterUsed();
            //        }
            //    }
            //    break;
        }
    }

    private void ChooseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && invantoryManager.isTheItemInInventory(1))
        {
            SetCurrentAbility(Ability.AbilityTypes.DashType);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && invantoryManager.isTheItemInInventory(2))
        {
            SetCurrentAbility(Ability.AbilityTypes.BoostType);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && invantoryManager.isTheItemInInventory(3))
        {
            SetCurrentAbility(Ability.AbilityTypes.ImmuneType);
        }

        
    }

    private void SetCurrentAbility(Ability.AbilityTypes abilityType)
    {
        if (!_isAbilityChoosed || (_isAbilityChoosed && abilityChoosenList[abilityType]))
        {
            abilityChoosenList[abilityType] = !abilityChoosenList[abilityType];

            for (int i = 0; abilitiesList.Count > i; i++)
            {
                if (abilityType == abilitiesList[i].AbilityType)
                {
                    CurrentAbility = CurrentAbility is null? abilitiesList[i] : null;
                    _isAbilityChoosed = !_isAbilityChoosed;
                    break;
                }
            }
        }
        else         {
            abilityChoosenList[abilityType] = !abilityChoosenList[abilityType];
            Debug.Log($"This is combo of: ");

            // Here need to choose on the combination of the ability and set the current ability to this combination
            foreach (var ability in abilitiesList)
            {
                //Debug.Log($"The ability {ability.AbilityType} is {abilityChoosenList[ability.AbilityType]} in the abilityChoosenList"); // this check all the abilities condition 
                if (abilityChoosenList[ability.AbilityType]) // if its dash and boost -> CurrentAbility = .... || if its dash and light -> CurrentAbility = ....
                {
                    Debug.Log(ability + " is choosed");
                    invantoryManager.useAbilityItem(ability.abilityNumber);  // To Do - remove it after have a new combo abilities
                    continue;
                }
            }
            // To Do - remove it after have the combo abilities
            ClearAbilityAfterUsed(); 
        }

    }

    private void ClearAbilityAfterUsed()
    {
        CurrentAbility = null;
        _isAbilityChoosed = false;
    }

    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
}