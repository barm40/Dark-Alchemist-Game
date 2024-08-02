using System;
using UnityEngine;
using UnityEngine.Serialization;

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
    public float speed = 3f;

    private Camera _camera;
    private float _cameraSize;
    
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("camTarget").transform;
        _camera = GetComponent<Camera>();
        _cameraSize = _camera.orthographicSize;
    }

    public void AddYAxis(float amount)
    {
        if (amount < 0) return;
        
        // move camera up and increase size by the square root of amount
        offset = new Vector3(0, amount * viewMultiplier * Time.deltaTime, -10);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraSize * (Mathf.Sqrt(1 + amount)), 0.1f);
    }

    void Update()
    {
        // Define target for camera
        var targetPosition = _target.position + offset;
        // Gradually move towards the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
