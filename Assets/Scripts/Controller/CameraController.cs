using UnityEngine;

/// <summary>
/// Follow player (camera target) with a slight buffer and a slight lerp
/// for dynamic feel
/// </summary>

public class CameraController : MonoBehaviour
{
    // Select target
    public Transform target;
    
    // Follow behind
    public Vector3 offset = new Vector3(0, 0, -10);
    public float speed = 3f;
    
    void Update()
    {
        // Define target for camera
        var targetPosition = target.position + offset;
        // Gradually move towards the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
