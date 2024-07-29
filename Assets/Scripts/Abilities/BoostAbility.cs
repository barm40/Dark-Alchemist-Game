using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAbility : Ability
{
    // times from stats
    public BoostAbility(Stats stats) : base(stats.ActiveTime, stats.CooldownTime)
    {
        AbilityType = AbilityTypes.BoostType;
        SetAbilityNumber();
    }
    
    private float _previousSpeedMultiplier;
    private float _previousJumpMultiplier;
    private float _previousLightMultiplier;

    public override void Activate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        _previousSpeedMultiplier = stats.MoveSpeedMultiplier;
        _previousJumpMultiplier = stats.JumpForceMultiplier;
        _previousLightMultiplier = stats.lightDamage;
        
        stats.MoveSpeedMultiplier *= stats.BoostMultiplier;
        stats.JumpForceMultiplier *= stats.BoostMultiplier;
        stats.lightDamage *= stats.BoostNegativeMultiplier;
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        stats.MoveSpeedMultiplier = _previousSpeedMultiplier;
        stats.JumpForceMultiplier = _previousJumpMultiplier;
        stats.lightDamage = _previousLightMultiplier;

        //parent.GetComponent<AbilityController>().CurrentAbility = new AbilityNone();
        //parent.GetComponent<AbilityController>()._isAbilityChoosed = false;
    }
}
