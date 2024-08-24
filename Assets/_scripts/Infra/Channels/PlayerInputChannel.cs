using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infra.Channels
{
    [CreateAssetMenu(fileName = "New Input Channel", menuName = "Input/Player Input")]
    public class PlayerInputChannel : ScriptableObject, PlayerInput.IPlayerActions
    {
        public event Action<float> MoveEvent;
        public event Action<float> PanCameraEvent;
        public event Action<int> SelectAbilityEvent;
        public event Action<bool> JumpEvent;
        public event Action<bool> DashEvent;
        public event Action<bool> PerformAbilityEvent;
        public event Action InteractEvent;
        
    
        private PlayerInput _input;

        private void OnEnable()
        {
            if (_input is null)
            {
                _input = new PlayerInput();
                _input.Player.SetCallbacks(this);
            }
        
            _input.Player.Enable();
        }

        private void OnDisable()
        {
            _input?.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
                MoveEvent?.Invoke(context.ReadValue<float>()); 
            else if (context.canceled)
                MoveEvent?.Invoke(0);
        } 

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed) JumpEvent?.Invoke(true);
            else if (context.canceled) JumpEvent?.Invoke(false);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed) InteractEvent?.Invoke();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed) DashEvent?.Invoke(true);
            else if (context.canceled) DashEvent?.Invoke(false);
        }
    
        public void OnSelectAbility(InputAction.CallbackContext context)
        {
            if (context.phase is InputActionPhase.Performed) SelectAbilityEvent?.Invoke((int)context.ReadValue<float>());
        }
    
        public void OnAbility(InputAction.CallbackContext context)
        {
            if (context.performed) PerformAbilityEvent?.Invoke(true);
            else if (context.canceled) PerformAbilityEvent?.Invoke(false);
        }
    
        public void OnPanCamera(InputAction.CallbackContext context) => PanCameraEvent?.Invoke(context.ReadValue<float>()); 
    }
}
