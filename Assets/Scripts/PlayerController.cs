using TMPro;
using UnityEngine;

[RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
public class PlayerController : MonoBehaviour
{
    private Stats _stats;
    private Rigidbody2D _rb2d;
    
    Items _items;
    
    [SerializeField] TMP_Text hpText;

    // for jump
    private float _coyoteTimer;
    private bool _isGrounded;
    
    
    private void OnEnable()
    {
        PlayerInLighDetect.UserInTheLighDelegate += RemoveHealth;
    }

    private void OnDisable()
    {
        PlayerInLighDetect.UserInTheLighDelegate -= RemoveHealth;
    }

    private void Start()
    {
        _stats = GetComponent<Stats>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        JumpVertical();
    }

    private void FixedUpdate()
    {
        MoveHorizontal();
    }
    
    private void MoveHorizontal()
    {
        var moveSpeed = Input.GetAxisRaw("Horizontal") * _stats.CurrentMoveSpeed;
        _rb2d.velocity = new Vector2(moveSpeed, _rb2d.velocity.y);
    } 
    
    private void JumpVertical()
    {
        if (_isGrounded)
        {
            _coyoteTimer = _stats.coyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }
        
        if (_coyoteTimer > 0f && Input.GetButtonDown("Jump"))
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _stats.CurrentJumpForce);
            Debug.Log("Jumping");
        }
        
        if (Input.GetButtonUp("Jump") && _rb2d.velocity.y > 0f)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _rb2d.velocity.y * 0.5f);

            _coyoteTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            _isGrounded = true;
            Debug.Log("Grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("platform"))
        {
            _isGrounded = false;
            Debug.Log("Jumping");
        }
    }

    void RemoveHealth()
    {
        if (_stats.Hp > 0)
        {
            PlayerInLighDetect.LightRemoveHealth(_stats);
            hpText.text = $"HP: {(int)_stats.Hp}";
            Debug.LogWarning($"Remove health in PlayerController");
        }
        else
        {
            Debug.LogWarning($"You are dead, Game Over!!");
        }
    }
}
