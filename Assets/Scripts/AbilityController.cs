using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]

public class AbilityController : MonoBehaviour
{
    private Stats _stats;
    private InvantoryManager invantoryManager;

    private float _abilityTime;
    private float _abilityCooldown;
    private AbilityNone defaultAbility;
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
    private bool isCombo = false;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        invantoryManager = FindObjectOfType<InvantoryManager>();
        defaultAbility = new AbilityNone();
        CurrentAbility = defaultAbility;
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
                    if (Input.GetKeyDown(ControlsManager.Controls["ability"]) && _isAbilityChoosed)
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
                        ClearAbility();
                    }
                    else
                    {
                        _abilityTime -= Time.deltaTime;
                        // Dash only works while key pressed
                        if (CurrentAbility.AbilityType == Ability.AbilityTypes.DashType && Input.GetKeyUp(ControlsManager.Controls["ability"]))
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && invantoryManager.IsTheItemInInventory(abilitiesList[0].abilityNumber))
        {
            SetCurrentAbility(Ability.AbilityTypes.DashType);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && invantoryManager.IsTheItemInInventory(abilitiesList[1].abilityNumber))
        {
            SetCurrentAbility(Ability.AbilityTypes.BoostType);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && invantoryManager.IsTheItemInInventory(abilitiesList[2].abilityNumber))
        {
            SetCurrentAbility(Ability.AbilityTypes.ImmuneType);
        }

        
    }

    private void SetCurrentAbility(Ability.AbilityTypes abilityType)
    {
        // if in combo mode and user diselect ability
        // if the user
        if (!_isAbilityChoosed)
        {
            abilityChoosenList[abilityType] = !abilityChoosenList[abilityType];

            for (int i = 0; abilitiesList.Count > i; i++)
            {
                if (abilityType == abilitiesList[i].AbilityType)
                {
                    CurrentAbility = CurrentAbility is null || CurrentAbility is AbilityNone? abilitiesList[i] : defaultAbility;
                    _isAbilityChoosed = true;
                    break;
                }
            }
        } 
        else if (_isAbilityChoosed && abilityChoosenList[abilityType] && !isCombo)
        {
            ClearAbility();
        }
        else if (isCombo)
        {
            abilityChoosenList[abilityType] = false;
            for (int i = 0; abilitiesList.Count > i; i++)
            {
                if (abilityChoosenList[abilitiesList[i].AbilityType])
                {
                    CurrentAbility = abilitiesList[i];
                }
            }
            Debug.Log(CurrentAbility);
            isCombo = false;
        }
        else
        {
            isCombo = true;
            abilityChoosenList[abilityType] = true;
            Debug.Log($"This is combo of: ");

            if (Input.GetKeyDown(abilityKey))
            {
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
                ClearAbility();
                // To Do - remove it after have the combo abilities
            }
        }
    }

    private void ClearAbility()
    {
        CurrentAbility = defaultAbility;
        _isAbilityChoosed = false;

        for (int i = 0; abilitiesList.Count > i; i++)
        {
            abilityChoosenList[abilitiesList[i].AbilityType] = false;
        }
    }

    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
}