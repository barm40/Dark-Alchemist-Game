using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    
    public DashAbility() : base(0.5f, 3f) {}
    
    [SerializeField] private const float DashMultiplier = 2f;

    private float _previousMultiplier;

    private void Start()
    {
        ability = AbilityTypes.DashAbility;
        itemInventoryNumber = 1;
    }

    public override void Activate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();
        _previousMultiplier = stats.MoveSpeedMultiplier;
        
        stats.MoveSpeedMultiplier = DashMultiplier;
    }

    public override void Deactivate(GameObject parent)
    {
        var stats = parent.GetComponent<Stats>();

        stats.MoveSpeedMultiplier = _previousMultiplier;
    }
}
