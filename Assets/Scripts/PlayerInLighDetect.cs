using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PlayerInLighDetect : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Light2D _light;

    public static event Action UserInTheLighDelegate;
    
   
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {

        Vector3 playerDirection = playerTransform.position - transform.position;
        Vector2 rayDirection2D = new Vector2(playerDirection.x, playerDirection.y);

        float rayDistance = _light.pointLightOuterRadius;

        Vector2 lightDirection = transform.up * transform.rotation.z;
        float angleOfPlayerFromLight = Vector2.Angle(lightDirection ,playerTransform.position - transform.position);
        

        if (Vector3.Distance(playerTransform.position, transform.position) < rayDistance && angleOfPlayerFromLight < _light.pointLightOuterAngle/2 )
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, rayDistance);

            if (hit.collider != null && hit.collider.name == "Player")
            {
                //Debug.Log($"hit Player!! {hit.collider.name}");
                UserInTheLighDelegate?.Invoke();
            }

            Debug.DrawRay(transform.position, rayDirection2D, Color.red);
        }

    }
}
