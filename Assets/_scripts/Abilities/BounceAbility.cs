using UnityEngine;
using UnityEngine.Animations;

public class BounceAbility : Ability
{
    private readonly float _bounceMultiplier;
    private readonly float _previousMultiplier;

    // get stats object and extract values from it
    public BounceAbility(Stats stats) : base(stats.ActiveTime)
    {
        ThisStats = stats;
        _bounceMultiplier = ThisStats.BounceMultiplier;
        _previousMultiplier = ThisStats.JumpForceMultiplier;
        AbilityType = AbilityTypes.BounceType;
        
        SetAbilityNumber();
    }

    public override void Activate(GameObject parent)
    {
        ThisStats.JumpForceMultiplier *= _bounceMultiplier;
    }

    public override void Deactivate(GameObject parent)
    {
        ThisStats.JumpForceMultiplier = _previousMultiplier;
    }
}
