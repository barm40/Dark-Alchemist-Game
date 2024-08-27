using Infra.Patterns;
using Infra.StatContainers;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This script will contain the stats for every entity in the game
/// The stats will be initialized for each entity accordingly
/// The current speed will be updated according to multiplier every tick.
/// </summary>

public class Stats : TrueSingleton<Stats>
{
    [Header("Stats Containers")] 
    [SerializeField, Tooltip("Movement Stats Container Scriptable Object")] 
    public MoveStatContainer playerMoveStats;
    [SerializeField, Tooltip("Jump Stats Container Scriptable Object")] 
    public JumpStatContainer playerJumpStats;
    [SerializeField, Tooltip("Dash Stats Container Scriptable Object")] 
    public DashStatContainer playerDashStats; 
    [SerializeField, Tooltip("Light Damage Stats Container Scriptable Object")] 
    public DamageContainer playerLightDamageContainer;

    public float abilityActiveTime;
}
