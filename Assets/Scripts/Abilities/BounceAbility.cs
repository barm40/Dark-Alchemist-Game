using UnityEngine;
using UnityEngine.Animations;

public class BounceAbility : Ability
{
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
            var stats = parent.GetComponent<Stats>();
            stats.JumpForceMultiplier *= _bounceMultiplier;
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        stats.JumpForceMultiplier = _previousMultiplier;
    }
}
