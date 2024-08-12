using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Health Event Channel", menuName = "Event Channel/Health")]
public class PlayerHealthChannel : ScriptableObject
{
    public float playerHealth = 100;
    
    [SerializeField] private float playerMaxHealth = 100f;
    
    [NonSerialized]
    public Action<float> HealthEvent;

    private void OnEnable()
    {
        playerHealth = playerMaxHealth;
        HealthEvent?.Invoke(playerHealth);
    }

    public void ChangeHealth(float amount)
    {
        playerHealth += amount * Time.deltaTime;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        HealthEvent?.Invoke(playerHealth);
    }
    
    public void QueryHealth()
    {
        HealthEvent?.Invoke(playerHealth);
    }
}