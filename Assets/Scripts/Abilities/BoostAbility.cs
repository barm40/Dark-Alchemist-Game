using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAbility : Ability, IAbilities
{
    private float _boostMultiplier;
    
    // times from stats
    public BoostAbility(Stats stats) : base(stats.ActiveTime, stats.CooldownTime)
    {
        AbilityType = AbilityTypes.BoostType;
    }
    
    private float _previousMultiplier;

    public override void Activate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        _boostMultiplier = stats.BoostMultiplier;
        _previousMultiplier = stats.MoveSpeedMultiplier;
        stats.MoveSpeedMultiplier = _boostMultiplier;
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        stats.MoveSpeedMultiplier = _previousMultiplier;
        
        parent.GetComponent<AbilityController>().CurrentAbility = new AbilityNone();
        parent.GetComponent<AbilityController>()._isAbilityChoosed = false;
    }
}
