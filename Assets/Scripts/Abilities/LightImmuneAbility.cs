using UnityEngine;

public class LightImmuneAbility : Ability
{
    private readonly float _prevLightDamage;
    private readonly float _shieldLightDamage;
    
    public LightImmuneAbility(Stats stats) : base(stats.ActiveTime)
    {
        ThisStats = stats;
        _prevLightDamage = ThisStats.lightDamage;
        _shieldLightDamage = ThisStats.shieldLightDamage;
        AbilityType = AbilityTypes.ImmuneType;

        SetAbilityNumber();
    }

    public override void Activate(GameObject parent)
    {
        ThisStats.lightDamage = _shieldLightDamage;
    }

    public override void Deactivate(GameObject parent)
    {
        ThisStats.lightDamage = _prevLightDamage;
    }
}
