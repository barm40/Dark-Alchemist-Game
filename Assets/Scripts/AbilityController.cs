using UnityEngine;

[RequireComponent(typeof(Stats))]

public class AbilityController : MonoBehaviour
{
    private Stats _stats;

    private float _abilityTime;
    private float _abilityCooldown;
    
    private AbilityState _abilityState = AbilityState.Ready;
    
    public Ability CurrentAbility { get; set; } = new DashAbility();
    [SerializeField] private KeyCode abilityKey;
    
    private void Start()
    {
        _stats = GetComponent<Stats>();
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
    
    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
}