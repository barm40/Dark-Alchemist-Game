using UnityEngine;

public abstract class Ability
{
    public float ActiveTime;
    public float CooldownTime;

    protected Ability(float activeTime, float cooldownTime)
    {
        ActiveTime = activeTime;
        CooldownTime = cooldownTime;
    }

    public abstract void Activate(GameObject parent);
    public abstract void Deactivate(GameObject parent);

}
