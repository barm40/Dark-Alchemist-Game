using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private enum DashState
    {
        Ready,
        Active,
        Cooldown
    }
    
    private DashState _dashState;
    
    private Stats _stats;
    private PlayerController _controller;
    
    private float _dashMultiplier;
    private float _previousMultiplier;

    private float _dashTime;
    private float _dashCooldown;

    private bool _dashIntent;
    private bool _hasLanded;
    
    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _controller = GetComponent<PlayerController>();
        _dashMultiplier = _stats.DashMultiplier;
        _previousMultiplier = _stats.MoveSpeedMultiplier;
        _dashState = DashState.Ready;
    }

    private void Update()
    {
        DashTimer();
        DoDash();
        CalcDash();
    }

    private void DoDash()
    {
        switch (_dashState)
        {
            case DashState.Ready:
            {
                if (_dashIntent)
                {
                    _dashState = DashState.Active;
                    _dashTime = _stats.DashActive;
                    _hasLanded = false;
                }
            }
                break;
            case DashState.Active:
            {
                // Dash only works while key pressed
                if (_dashTime <= 0)
                {
                    _dashState = DashState.Cooldown;
                    _dashCooldown = _stats.DashCooldown;
                }
                else
                {
                    if (!_dashIntent)
                    {
                        _dashState = DashState.Cooldown;
                        _dashCooldown = _stats.DashCooldown;
                    }
                }
            }
                break;
            case DashState.Cooldown:
            {
                if (_dashCooldown <= 0 && _hasLanded)
                    _dashState = DashState.Ready;
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DashTimer()
    {
        if (_dashTime > 0)
            _dashTime -= Time.deltaTime;
        if (_dashCooldown > 0)
            _dashCooldown -= Time.deltaTime;
        if (_controller.IsGrounded())
            _hasLanded = true;
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            _dashIntent = true;
        }
        else if (context.canceled)
        {
            _dashIntent = false;
        }
    }
    
    private void CalcDash()
    {
        if (_dashState == DashState.Active)
        {
            _stats.MoveSpeedMultiplier = Mathf.Lerp(_stats.MoveSpeedMultiplier, _dashMultiplier, 1f);
        }
        else
        {
            _stats.MoveSpeedMultiplier = Mathf.Lerp(_stats.MoveSpeedMultiplier, _previousMultiplier, 5f * Time.deltaTime);
        }
    }
}
