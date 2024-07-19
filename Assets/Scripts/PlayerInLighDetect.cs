using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInLighDetect : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    Light2D _light;
    private void Start()
    {
        PlayerTransform = FindObjectOfType<PlayerController>().transform;
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {

        Vector3 playerDirection = PlayerTransform.position - transform.position;
        Vector2 rayDirection2D = new Vector2(playerDirection.x, playerDirection.y);

        float rayDistance = _light.pointLightOuterRadius;

        if(Vector3.Distance(PlayerTransform.position,transform.position) < rayDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, rayDistance);

            if (hit.collider != null && hit.collider.name == "Player")
            {
                Debug.Log($"hit Player!! {hit.collider.name}");
            }

            Debug.DrawRay(transform.position, rayDirection2D, Color.red);
        }
        
    }
}
