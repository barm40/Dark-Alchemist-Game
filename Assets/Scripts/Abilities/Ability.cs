using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

public abstract class Ability : Items
{
    public enum AbilityTypes { DashType, BoostType, ImmuneType }
    public AbilityTypes AbilityType { get; protected set; }
    
    public readonly float ActiveTime;
    public readonly float CooldownTime;
    private Stats _stats;

    protected Ability(float activeTime, float cooldownTime)
    {
        ActiveTime = activeTime;
        CooldownTime = cooldownTime;
    }
    
    public abstract void Activate(GameObject parent);
    public abstract void Deactivate(GameObject parent);
}
