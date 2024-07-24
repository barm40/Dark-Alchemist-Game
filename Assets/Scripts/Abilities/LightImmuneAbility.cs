using UnityEngine;

public class LightImmuneAbility : Ability
{
    private readonly float _prevLightDamage;
    private readonly float _shieldLightDamage;
    
    public LightImmuneAbility(Stats stats) : base(stats.ActiveTime, stats.CooldownTime)
    {
        AbilityType = AbilityTypes.ImmuneType;
        _prevLightDamage = stats.lightDamage;
        _shieldLightDamage = stats.shieldLightDamage;
    }

    public override void Activate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        
        stats.lightDamage = _shieldLightDamage;
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        
        stats.lightDamage = _prevLightDamage;
        
        parent.GetComponent<AbilityController>().CurrentAbility = new AbilityNone();
        parent.GetComponent<AbilityController>()._isAbilityChoosed = false;
    }
}
