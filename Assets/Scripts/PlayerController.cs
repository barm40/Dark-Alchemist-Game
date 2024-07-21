using UnityEngine;

[RequireComponent(typeof(Stats))]

public class PlayerController : MonoBehaviour
{
    private Stats _stats;
    
    [SerializeField] private float lightDemage = 5f;
    

    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerInLighDetect.UserInTheLighDelegate += RemoveHealth;
    }

    private void Start()
    {
        _stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHorizontal();
    }
    
    private void MoveHorizontal()
    { 
        var moveAmount = Input.GetAxis("Horizontal") * _stats.CurrentMoveSpeed; 
        transform.Translate(moveAmount, 0, 0, Space.World);
    }



    void RemoveHealth()
    {
        if (_stats.hp > 0)
        {
            _stats.LightRemoveHealth(lightDemage * Time.deltaTime);
            Debug.Log(_stats.hp);
        }
        else
        {
            Debug.Log($"You are dead, Game Over!!");
        }
    }
    
    
}
