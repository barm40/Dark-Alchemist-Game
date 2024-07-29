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
    [SerializeField] private ParticleSystem[] abilitiesVFX;
    private ParticleSystem CurrentAbilityVFX = null;
    private bool isCombo = false;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        invantoryManager = FindObjectOfType<InvantoryManager>();
        defaultAbility = new AbilityNone();
        CurrentAbility = defaultAbility;
        CurrentAbilityVFX = null;
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
                        GetAbilityVFX(CurrentAbility.AbilityType)?.Play();
                    }
                }
                break;
            case AbilityState.Active:
                {
                    if (_abilityTime <= 0)
                    {
                        CurrentAbility.Deactivate(gameObject);
                        _abilityState = AbilityState.Ready;
                        GetAbilityVFX(CurrentAbility.AbilityType)?.Stop();
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

        if (isCombo)
        {
            UseComboAbility();
        }
    }

    private void SetCurrentAbility(Ability.AbilityTypes abilityType)
    {
        // if in combo mode and user diselect ability
        // if the user
        if (!_isAbilityChoosed)
        {
            abilityChoosenList[abilityType] = true;

            for (int i = 0; abilitiesList.Count > i; i++)
            {
                if (abilityType == abilitiesList[i].AbilityType)
                {
                    CurrentAbility = CurrentAbility is null || CurrentAbility is AbilityNone ? abilitiesList[i] : defaultAbility;
                    _isAbilityChoosed = true;
                    break;
                }
            }
        }
        else if (_isAbilityChoosed && abilityChoosenList[abilityType] && !isCombo)
        {
            ClearAbility();
        }
        else if (isCombo && abilityChoosenList[abilityType])
        {
            abilityChoosenList[abilityType] = false;
            for (int i = 0; abilitiesList.Count > i; i++)
            {
                if (abilityChoosenList[abilitiesList[i].AbilityType])
                {
                    CurrentAbility = abilitiesList[i];
                }
            }
            isCombo = false;
        }
        else
        {
            isCombo = true;
            abilityChoosenList[abilityType] = true;
        }
    }

    /// <summary>
    /// This function for use when have combo mode element,
    /// actually when we will have more abilities we need to use it to set the combo ability and from here call to 'Do ability' 
    /// </summary>
    private void UseComboAbility()
    {
        if (Input.GetKeyDown(ControlsManager.Controls["ability"]))
        {
            foreach (var ability in abilitiesList)
            {
                if (abilityChoosenList[ability.AbilityType])
                {
                    Debug.Log($"the ability {ability.AbilityType} is choosed");
                    invantoryManager.useAbilityItem(ability.abilityNumber);
                    abilityChoosenList[ability.AbilityType] = false;
                }
            }
            isCombo = false;
            ClearAbility();
            // To Do - remove it after have the combo abilities
        }
    }

    private ParticleSystem GetAbilityVFX(Ability.AbilityTypes abilityTypes)
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
        CurrentAbility = defaultAbility;
        CurrentAbilityVFX =  null;
        _isAbilityChoosed = false;

        for (int i = 0; abilitiesList.Count > i; i++)
        {
            abilityChoosenList[abilitiesList[i].AbilityType] = false;
        }
    }

    private enum AbilityState
    {
        Ready,
        Active
    }
}