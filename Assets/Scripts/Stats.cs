using UnityEngine;


/// <summary>
/// This script will contain the stats for every entity in the game
/// The stats will be initialized for each entity accordingly
/// The current speed will be updated according to multiplier every tick.
/// </summary>

public class Stats : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 1;
    public float MoveSpeedMultiplier { get; set; }
    public float CurrentMoveSpeed { get; private set; }
    
    public float hp { get; private set; } = 100f;

    private void Start()
    {
        MoveSpeedMultiplier = 1f;
    }

    private void Update()
    {
        CurrentMoveSpeed = 
            baseMoveSpeed * MoveSpeedMultiplier * Time.deltaTime;
    }

    public void LightRemoveHealth(float amount)
    {
        hp -= amount;
    }
}
