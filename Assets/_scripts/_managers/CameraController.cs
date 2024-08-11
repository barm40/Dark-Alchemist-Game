using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Follow player (camera target) with a slight buffer and a slight lerp
/// for dynamic feel
/// </summary>

public class CameraController : MonoBehaviour
{
    // Select target
    private Transform _target;
    
    // Follow behind
    public Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float viewMultiplier = 200f;
    public float speed = 2f;

    private Camera _camera;
    private float _cameraSize;
    
    // Pan up controls
    private bool _panIntent;
    private float _panAmount;
    
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("camTarget").transform;
        _camera = GetComponent<Camera>();
        _cameraSize = _camera.orthographicSize;
    }

    private void FixedUpdate()
    {
        PanCamera();
    }

    public void PanInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            _panAmount = context.ReadValue<float>(); 
        }
        else if (context.canceled)
        {
            _panAmount = 0;
        }
    }
    
    private void Update()
    {
        // Define target for camera
        var targetPosition = _target.position + offset;
        // Gradually move towards the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
    
    private void PanCamera()
    {
        if (_panAmount < 0) return;
        
        // move camera up and increase size by the square root of amount
        offset = new Vector3(0, _panAmount * viewMultiplier * Time.deltaTime, -10);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraSize * (Mathf.Sqrt(1 + _panAmount)), 0.9f * Time.deltaTime);
    }
}
