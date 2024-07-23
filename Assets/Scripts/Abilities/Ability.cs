using UnityEngine;

public abstract class Ability : Items
{
    public enum AbilityTypes { DashAbility }
    public float ActiveTime;
    public float CooldownTime;
    public AbilityTypes ability { get; protected set; }

    protected Ability(float activeTime, float cooldownTime)
    {
        ActiveTime = activeTime;
        CooldownTime = cooldownTime;
    }

    public abstract void Activate(GameObject parent);
    public abstract void Deactivate(GameObject parent);

}
