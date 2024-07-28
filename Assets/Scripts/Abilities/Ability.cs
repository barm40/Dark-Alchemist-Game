using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

public abstract class Ability
{
    public enum AbilityTypes { None, DashType, BoostType, ImmuneType }
    public AbilityTypes AbilityType { get; protected set; }
    
    public readonly float ActiveTime;
    public readonly float CooldownTime;
    public static int amountOfAbilities = 0;
    public int abilityNumber;


    protected Ability(float activeTime, float cooldownTime)
    {
        ActiveTime = activeTime;
        CooldownTime = cooldownTime;
        amountOfAbilities++;
        abilityNumber = amountOfAbilities;
    }
    
    public abstract void Activate(GameObject parent);
    public abstract void Deactivate(GameObject parent);
}
