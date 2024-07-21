using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PlayerInLighDetect : MonoBehaviour
{
    [SerializeField,Tooltip("Active the rayCast hit debug to know in which object its hit")] bool allowRayCastHitDebug = false;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 rayCastLightStartPosition;
    [SerializeField] LayerMask layerMask;
    Light2D _light;

    public static event Action UserInTheLighDelegate;
    /// <summary>
    /// This script detect if the player is in the light view,
    /// Set the script as component under the light object
    /// </summary>
   
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        _light = GetComponent<Light2D>();
        Debug.Log(layerMask.value);
    }

    private void Update()
    {

        Vector3 playerHeadDirection = playerTransform.position - transform.position + new Vector3(0,transform.localScale.y/2,0);
        Vector2 rayDirection2D = new Vector2(playerHeadDirection.x, playerHeadDirection.y);

        float rayDistance = _light.pointLightOuterRadius;

        Vector2 lightDirection = transform.up * Math.Abs(transform.rotation.z) ;

        float angleOfPlayerFromLight = Vector2.Angle(lightDirection ,playerTransform.position - transform.position - new Vector3(0, transform.localScale.y / 2, 0));
        

        if (Vector3.Distance(playerTransform.position, transform.position) < rayDistance && angleOfPlayerFromLight < _light.pointLightOuterAngle/2 )
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + rayCastLightStartPosition, playerHeadDirection, rayDistance,layerMask);
            if (allowRayCastHitDebug)
            {
                Debug.Log(hit.collider.name);
            }
            

            if (hit.collider != null && hit.collider.name == "Player")
            {
                UserInTheLighDelegate?.Invoke();
            }

            Debug.DrawRay(transform.position, rayDirection2D, Color.red);
        }

    }
}
