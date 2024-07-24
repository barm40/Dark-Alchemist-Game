using UnityEngine;

[RequireComponent(typeof(Stats))]

public class AbilityController : MonoBehaviour
{
    private Stats _stats;
    private InvantoryManager invantoryManager;

    private float _abilityTime;
    private float _abilityCooldown;
    
    private AbilityState _abilityState = AbilityState.Ready;
    
    public Ability CurrentAbility { get; set; }
    [SerializeField] private KeyCode abilityKey = KeyCode.LeftShift;
    
    public bool _isAbilityChoosed { private get; set; }

    private void Start()
    {
        _stats = GetComponent<Stats>();
        invantoryManager = FindObjectOfType<InvantoryManager>();
        CurrentAbility = new DashAbility(_stats);
        _isAbilityChoosed = true;
    }

    private void Update()
    {
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
                }
            }
                break;
            case AbilityState.Active:
            {
                if(_abilityTime <= 0)
                {
                    CurrentAbility.Deactivate(gameObject);
                    _abilityState = AbilityState.Cooldown;
                    _abilityCooldown = CurrentAbility.CooldownTime;
                }
                else
                {
                    _abilityTime -= Time.deltaTime;
                    // Dash only works while key pressed
                    if (CurrentAbility.AbilityType == Ability.AbilityTypes.DashType && Input.GetKeyUp(abilityKey))
                    {
                        CurrentAbility.Deactivate(gameObject);
                        _abilityState = AbilityState.Cooldown;
                        _abilityCooldown = CurrentAbility.CooldownTime;
                    }
                }
            }
                break;
            case AbilityState.Cooldown:
            {
                if (_abilityCooldown > 0)
                    _abilityCooldown -= Time.deltaTime;
                else
                {
                    _abilityState = AbilityState.Ready;
                }
            }
                break;
        }
    }
    
    private void ChooseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (invantoryManager.isTheItemInInventory(1))
            {
                _isAbilityChoosed = true;
                //CurrentAbility = new DashAbility();
            }
            else
            {
                _isAbilityChoosed = false;
            }
        }
    }
    
    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
}