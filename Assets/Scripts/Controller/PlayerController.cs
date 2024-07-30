using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
public class PlayerController : MonoBehaviour, ISaveData
{
    private Stats _stats;
    private Rigidbody2D _rb2d;
    
    Items _items;
    
    [SerializeField] private TMP_Text hpText;

    // for jump
    public Vector2 boxSize;
    public LayerMask groundLayer;
    public float castDistance;
    
    private bool _isJumping;
    private float _jumpBufferTimer;
    private float _coyoteTimer;
    
    // for movement and animation
    private float _horizontal;
    private bool _isFacingRight = true;

    // keep the player between levels

    // Hit Effect
    private Volume hitVolume;
    private Bloom hitBloom;
    private bool isHitEffectActive;

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
        hpText.text = "HP: " + (int)_stats.Hp;
        hitVolume = transform.GetComponentInChildren<Volume>();
        hitVolume.profile.TryGet<Bloom>(out hitBloom);
    }

    private void Update()
    {
        JumpVertical();
    }

    private void FixedUpdate()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        
        MoveHorizontal();
        Flip();
    }
    
    private void MoveHorizontal()
    {
        var moveSpeed = _horizontal * _stats.CurrentMoveSpeed;
        _rb2d.velocity = new Vector2(moveSpeed, _rb2d.velocity.y);
    } 
    
    private void JumpVertical()
    {
        if (IsGrounded())
        {
            _coyoteTimer = _stats.coyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(ControlsManager.Instance.Controls["jump"]))
        {
            _jumpBufferTimer = _stats.jumpBufferTime;
        }
        else
        {
            _jumpBufferTimer -= Time.deltaTime;
        }
        
        if (_coyoteTimer > 0f && _jumpBufferTimer > 0f && !_isJumping)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _stats.CurrentJumpForce);
            Debug.Log("Jumping");

            _jumpBufferTimer = 0f;
            StartCoroutine(JumpCooldown());
        }
        
        if (Input.GetKeyUp(ControlsManager.Instance.Controls["jump"]) && _rb2d.velocity.y > 0f)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x,  -_rb2d.velocity.y * 0.1f);

            _coyoteTimer = 0f;
        }
    }
    
    private IEnumerator JumpCooldown()
    {
        _isJumping = true;
        yield return new WaitForSeconds(0.4f);
        _isJumping = false;
    }
    
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }
    
    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    void RemoveHealth()
    {
        if (_stats.Hp > 0)
        {
            PlayerInLighDetect.LightRemoveHealth(_stats);
            hpText.text = $"HP: {(int)_stats.Hp}";
            if (!isHitEffectActive)
            {
                StartCoroutine(GetHitLightEffect());
            }
        }
        else
        {
            //TO DO
            Debug.LogWarning($"You are dead, Game Over!!");
        }
    }

    IEnumerator GetHitLightEffect()
    {
        isHitEffectActive = true;
        hitVolume.enabled = true;
        hitBloom.scatter.value = 0.400f;
        yield return new WaitForSeconds(0.5f);
        hitBloom.scatter.value = 0.125f;
        yield return new WaitForSeconds(0.5f);
        isHitEffectActive = false;
        hitVolume.enabled = false;
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = transform.position;
    }
}
