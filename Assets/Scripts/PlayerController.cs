using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
public class PlayerController : MonoBehaviour
{
    private Stats _stats;
    [SerializeField] private float lightDemage = 5f;

    Items _items;

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
            _stats.LightRemoveHealth(lightDemage * Time.deltaTime);
            Debug.LogWarning($"Remove health in PlayerController");
        }
        else
        {
            Debug.LogWarning($"You are dead, Game Over!!");
        }
    }
}
