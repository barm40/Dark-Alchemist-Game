using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Stats _stats;
    
    // Start is called before the first frame update
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
}
