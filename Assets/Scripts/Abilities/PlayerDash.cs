using System;
using UnityEngine;

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
    
    private float _dashMultiplier;
    private float _previousMultiplier;

    private float _dashTime;
    private float _dashCooldown;
    
    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _dashMultiplier = _stats.DashMultiplier;
        _previousMultiplier = _stats.MoveSpeedMultiplier;
        _dashState = DashState.Ready;
    }

    private void Update()
    {
        DoDash();
    }

    private void DoDash()
    {
        switch (_dashState)
        {
            case DashState.Ready:
            {
                if (Input.GetKeyDown(ControlsManager.Controls["dash"]))
                {
                    Activate();
                    _dashState = DashState.Active;
                    _dashTime = _stats.DashActive;
                }
            }
                break;
            case DashState.Active:
            {
                // Dash only works while key pressed
                if (_dashTime <= 0)
                {
                    Deactivate();
                    _dashState = DashState.Cooldown;
                    _dashCooldown = _stats.DashCooldown;
                }
                else
                {
                    _dashTime -= Time.deltaTime;
                    if (Input.GetKeyUp(ControlsManager.Controls["dash"]))
                    {
                        Deactivate();
                        _dashState = DashState.Cooldown;
                        _dashCooldown = _stats.DashCooldown;
                    }
                }
            }
                break;
            case DashState.Cooldown:
            {
                if (_dashCooldown > 0)
                    _dashCooldown -= Time.deltaTime;
                else
                {
                    _dashState = DashState.Ready;
                }
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void Activate()
    {
        _stats.MoveSpeedMultiplier = _dashMultiplier;
    }

    private void Deactivate()
    {
        _stats.MoveSpeedMultiplier = _previousMultiplier;
    }
}
