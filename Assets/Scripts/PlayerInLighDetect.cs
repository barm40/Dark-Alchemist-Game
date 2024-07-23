using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInLighDetect : MonoBehaviour
{
    [SerializeField,Tooltip("Active the rayCast hit debug to know in which object its hit")] bool allowDebug = false;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 rayCastPlayerFixPosition = new Vector3(0f,0.09f,0f);
    [SerializeField] Vector3 PlayerLegFixPosition = new Vector3(0f,-0.26f,0f);
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
        layerMask = LayerMask.GetMask("Default", "Player");
    }

    private void Update()
    {

        Vector3 playerHeadDirection = playerTransform.position + rayCastPlayerFixPosition - transform.position;
        Vector2 rayDirection2D = new Vector2(playerHeadDirection.x, playerHeadDirection.y);

        float rayDistance = _light.pointLightOuterRadius;

        Vector2 lightDirection = transform.up * Math.Abs(transform.rotation.z) ;

        float angleOfPlayerFromLight = Vector2.Angle(lightDirection ,playerTransform.position - transform.position);
        float angleOfLegPlayerFromLight = Vector2.Angle(lightDirection, playerTransform.position + PlayerLegFixPosition - transform.position);
        
        bool playerInTheLightAngle = (angleOfPlayerFromLight < _light.pointLightOuterAngle / 2) || (angleOfLegPlayerFromLight < _light.pointLightOuterAngle / 2);
        bool playerInTheLightDistance = Vector3.Distance(playerTransform.position, transform.position) < rayDistance;
        bool thePlayerInLightArea = playerInTheLightAngle && playerInTheLightDistance;

        if (allowDebug)
        {
            Debug.DrawLine(transform.position, playerTransform.position + PlayerLegFixPosition, Color.yellow);
            Debug.Log($"The angle of the player from the user: {angleOfPlayerFromLight}");
            Debug.Log(
                $"The player is in the area: {thePlayerInLightArea}\n" +
                $"The distatnce in range: {playerInTheLightDistance}, the distance is: {Vector3.Distance(playerTransform.position, transform.position)}\n" +
                $"The player in the light area: {playerInTheLightAngle} - angle from head: {angleOfPlayerFromLight}, angle from leg: {angleOfLegPlayerFromLight}");
        }

        if (thePlayerInLightArea)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + rayCastPlayerFixPosition, playerHeadDirection, rayDistance);
            if (allowDebug)
            {
                Debug.DrawRay(transform.position, rayDirection2D, Color.red);
                Debug.Log(hit.collider.gameObject.name);
            }
            
            if (hit.collider != null && hit.collider.name == "Player")
            {
                UserInTheLighDelegate?.Invoke();
                if (allowDebug)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log($"The angle of the player from the user: {angleOfPlayerFromLight}");
                }
            }
        }
    }
    
    public static void LightRemoveHealth(Stats stats)
    {
        stats.hp -= (stats.lightDamage * Time.deltaTime);
    }
}
