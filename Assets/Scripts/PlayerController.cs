using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
public class PlayerController : MonoBehaviour
{
    private Stats _stats;
    
    Items _items;
    
    [SerializeField] TMP_Text hpText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerInLighDetect.UserInTheLighDelegate += RemoveHealth;
    }

    private void OnDisable()
    {
        PlayerInLighDetect.UserInTheLighDelegate -= RemoveHealth;
    }

    private void Start()
    {
        _stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHorizontal();
    }
    
    private void MoveHorizontal()
    { 
        var moveAmount = Input.GetAxis("Horizontal") * _stats.CurrentMoveSpeed; 
        transform.Translate(moveAmount, 0, 0, Space.World);
    }

    void RemoveHealth()
    {
        if (_stats.hp > 0)
        {
            PlayerInLighDetect.LightRemoveHealth(_stats);
            hpText.text = $"HP: {(int)_stats.hp}";
            Debug.LogWarning($"Remove health in PlayerController");
        }
        else
        {
            Debug.LogWarning($"You are dead, Game Over!!");
        }
    }
}
