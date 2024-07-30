using UnityEngine;
using UnityEngine.Animations;

public class BounceAbility : Ability
{
    private int _dashUses;
        
    private readonly float _bounceMultiplier;
    private readonly float _previousMultiplier;

    // get stats object and extract values from it
    public BounceAbility(Stats stats) : base(stats.ActiveTime, stats.CooldownTime)
    {
        _bounceMultiplier = stats.BounceMultiplier;
        _previousMultiplier = stats.JumpForceMultiplier;
        AbilityType = AbilityTypes.BounceType;
        SetAbilityNumber();
    }

    public override void Activate(GameObject parent)
    {
        if (_dashUses > 0)
        {
            var stats = parent.GetComponent<Stats>();
            stats.JumpForceMultiplier *= _bounceMultiplier;
        }
        else
        {
            ResetAbility(parent);
        }
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();

        stats.JumpForceMultiplier = _previousMultiplier;
    }

    private static void ResetAbility(GameObject parent)
    {
        //parent.GetComponent<AbilityController>().CurrentAbility = new AbilityNone();
        //parent.GetComponent<AbilityController>()._isAbilityChoosed = false;
    }
}
