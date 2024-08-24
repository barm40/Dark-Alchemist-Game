using System;
using Infra.Channels;
using Infra.StatContainers;
using UnityEngine;

namespace _managers
{
    public class DashController : MonoBehaviour
    {
        [Header("Input Channel")]
        [SerializeField, Tooltip("Scriptable Object Channel to control Input")] 
        private PlayerInputChannel inputChannel;
        [Header("Stat Container")]
        [SerializeField, Tooltip("Scriptable Object Stats Container (Dash)")]
        private DashStatContainer dashStats;
        [SerializeField, Tooltip("Scriptable Object Stats Container (Move)")]
        private MoveStatContainer moveStats;
    
        private enum DashState
        {
            Ready,
            Active,
            Cooldown
        }
    
        private DashState _dashState;
    
        private PlayerController _controller;
    
        private float _previousMultiplier;

        private float _dashTime;
        private float _dashCooldown;

        private bool _dashIntent;
        private bool _hasLanded;

        private void OnEnable()
        {
            inputChannel.DashEvent += DashInput;
        }

        private void OnDisable()
        {
            inputChannel.DashEvent -= DashInput;
        }
    
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
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
                    if (_dashIntent)
                    {
                        TransitionToActiveState();
                    }
                    break;
                case DashState.Active:
                    if (_dashTime <= 0 || !_dashIntent)
                    {
                        TransitionToCooldownState();
                    }
                    break;
                case DashState.Cooldown:
                    if (_dashCooldown <= 0 && _hasLanded)
                    {
                        _dashState = DashState.Ready;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TransitionToActiveState()
        {
            _dashState = DashState.Active;
            _dashTime = dashStats.dashStats.dashActive;
            _hasLanded = false;
        }

        private void TransitionToCooldownState()
        {
            _dashState = DashState.Cooldown;
            _dashCooldown = dashStats.dashStats.dashCooldown;
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

        private void DashInput(bool intent)
        {
            _dashIntent = intent;
        }
    
        private void CalcDash()
        {
            if (_dashState == DashState.Active)
            {
                moveStats.NewSpeed(Mathf.Lerp(moveStats.moveStats.moveSpeedMultiplier,dashStats.dashStats.dashMultiplier,1f));
            }
            else
            {
                moveStats.ResetSpeed();
            }
        }
    }
}
