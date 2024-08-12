using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input Channel", menuName = "Input/Player Input")]
public class PlayerInputChannel : ScriptableObject, PlayerInput.IPlayerActions
{
    public Action<float> moveEvent;
    public Action<float> panCameraEvent;
    public Action<bool> jumpEvent;
    public Action<float> selectAbilityEvent;
    public Action dashEvent;
    public Action interactEvent;
    public Action performAbilityEvent;
    
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

    public void OnMove(InputAction.CallbackContext context) => moveEvent?.Invoke(context.ReadValue<float>()); 

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) jumpEvent?.Invoke(true);
        else if (context.canceled) jumpEvent?.Invoke(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Performed) interactEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Performed) dashEvent?.Invoke();
    }
    
    public void OnSelectAbility(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Performed) selectAbilityEvent?.Invoke(context.ReadValue<float>());
    }
    
    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Performed) performAbilityEvent?.Invoke();
    }
    
    public void OnPanCamera(InputAction.CallbackContext context) => panCameraEvent?.Invoke(context.ReadValue<float>()); 
}
