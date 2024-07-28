using UnityEngine;
using UnityEngine.Animations;

public class DashAbility : Ability
{
    private int _dashUses;
        
    private readonly float _dashMultiplier;
    private readonly float _previousMultiplier;

    // get stats object and extract values from it
    public DashAbility(Stats stats) : base(stats.DashActive, stats.DashCooldown)
    {
        _dashUses = stats.DashUses;
        _dashMultiplier = stats.DashMultiplier;
        _previousMultiplier = stats.MoveSpeedMultiplier;
        AbilityType = AbilityTypes.DashType;
        //Debug.Log($"This {AbilityType} is number: {abilityNumber}");
    }

    public override void Activate(GameObject parent)
    {
        if (_dashUses > 0)
        {
            var stats = parent.GetComponent<Stats>();
            stats.MoveSpeedMultiplier = _dashMultiplier;
        }
        else
        {
            ResetAbility(parent);
        }
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();

        stats.MoveSpeedMultiplier = _previousMultiplier;
        _dashUses--;
    }

    private static void ResetAbility(GameObject parent)
    {
        //parent.GetComponent<AbilityController>().CurrentAbility = new AbilityNone();
        //parent.GetComponent<AbilityController>()._isAbilityChoosed = false;
    }
}
