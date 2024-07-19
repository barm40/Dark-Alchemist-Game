using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script will contain the stats for every entity in the game
/// The stats will be initialized for each entity accordingly
/// The current speed will be updated according to multiplier every tick.
/// </summary>

public class Stats : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed;
    private float MoveSpeedMultiplier { get; set; }
    public float CurrentMoveSpeed { get; private set; }

    private void Start()
    {
        MoveSpeedMultiplier = 1f;
    }

    private void Update()
    {
        CurrentMoveSpeed = 
            baseMoveSpeed * MoveSpeedMultiplier * Time.deltaTime;
    }
}
