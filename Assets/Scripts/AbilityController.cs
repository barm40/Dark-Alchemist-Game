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
    [SerializeField] private KeyCode abilityKey;
    bool isAbilityChoosed = false;

    private void Start()
    {
        _stats = GetComponent<Stats>();
        invantoryManager = FindObjectOfType<InvantoryManager>();
        CurrentAbility = new BoostAbility(_stats);
    }

    private void Update()
    {
        if(Input.GetKeyDown(abilityKey) && isAbilityChoosed)
        {
            DoAbility();
        }
    }

    private void DoAbility()
    {
        switch (_abilityState)
        {
            case AbilityState.Ready:
            {
                if (Input.GetKeyDown(abilityKey))
                {
                    CurrentAbility.Activate(gameObject);
                    _abilityState = AbilityState.Active;
                    _abilityTime = CurrentAbility.ActiveTime;
                }
            }
                break;
            case AbilityState.Active:
            {
                if (_abilityTime > 0)
                    _abilityTime -= Time.deltaTime;
                else
                {
                    CurrentAbility.Deactivate(gameObject);
                    _abilityState = AbilityState.Cooldown;
                    _abilityCooldown = CurrentAbility.CooldownTime;
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
                isAbilityChoosed = true;
                CurrentAbility = new DashAbility();
            }
            else
            {
                isAbilityChoosed = false;
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