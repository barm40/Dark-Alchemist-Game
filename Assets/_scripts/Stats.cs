using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// This script will contain the stats for every entity in the game
/// The stats will be initialized for each entity accordingly
/// The current speed will be updated according to multiplier every tick.
/// </summary>

public class Stats : MonoBehaviour
{
    // player related fields
    // move
    [SerializeField] private float baseMoveSpeed = 100f;
    public float MoveSpeedMultiplier { get; set; } = 1f;
    public float CurrentMoveSpeed { get; private set; }
    
    // jump
    [SerializeField] private float baseJumpForce = 150f;
    public float JumpForceMultiplier { get; set; } = 1f;
    public float CurrentJumpForce { get; private set; }
    public float coyoteTime = .2f;
    public float jumpBufferTime = .2f;

    
    public float Hp { get; set; } = 100f;
    
    // light damage related
    public float lightDamage = -20;
    public float shieldLightDamage = 0f;
    
    // general ability related stats 
    public float ActiveTime { get; private set; } = 15f;
    public float CooldownTime { get; private set; } = 15f;
    
    // dash specific stats 
    public float DashActive { get; private set; } = .3f;
    [SerializeField] public float DashCooldown { get; private set; } = .7f;
    [SerializeField] public float DashMultiplier { get; set; } = 3f;
    
    // boost specific stats
    public float BoostMultiplier { get; set; } = 1.2f;
    public float BoostNegativeMultiplier { get; set; } = 0.5f;
    
    // bounce specific stats
    public float BounceMultiplier { get; set; } = 1.5f;


    private void FixedUpdate()
    {
        CurrentMoveSpeed = 
            baseMoveSpeed * MoveSpeedMultiplier * Time.fixedDeltaTime;
        CurrentJumpForce = 
            baseJumpForce * JumpForceMultiplier * Time.fixedDeltaTime;
    }

    // public void LoadData(GameData data)
    // {
    //     Hp = data.healthPoints;
    // }
    //
    // public void SaveData(GameData data)
    // {
    //     data.healthPoints = Hp;
    // }
}
