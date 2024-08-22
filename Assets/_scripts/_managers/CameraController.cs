using System;
using Infra.Channels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _managers
{
    /// <summary>
    /// Follow player (camera target) with a slight buffer and a slight lerp
    /// for dynamic feel
    /// </summary>

    public class CameraController : MonoBehaviour
    {

        [SerializeField,Header("Event Channels"), Tooltip("Select player input Scriptable Object")]
        private PlayerInputChannel inputChannel;
        
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

        private void OnEnable()
        {
            inputChannel.panCameraEvent += PanInput;
        }

        private void OnDisable()
        {
            inputChannel.panCameraEvent -= PanInput;
        }

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

        public void PanInput(float amount)
        {
            _panAmount = amount;
        }
    
        private void LateUpdate()
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
}
