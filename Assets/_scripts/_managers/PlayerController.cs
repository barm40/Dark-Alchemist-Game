using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
public class PlayerController : MonoBehaviour
{
    [Header("Scriptable Object Channels")]
    [SerializeField, Tooltip("Scriptable Object Channel to control Input")] 
    private PlayerInputChannel inputChannel;
    [SerializeField, Tooltip("Scriptable Object Channel player HP")] 
    private PlayerHealthChannel healthChannel;
    
    private Stats _stats;
    private Rigidbody2D _rb2d;
    private AbilityController _abilityController;
    private DeathMenu _deathMenu;
    
    Items _items;
    
    // for jump
    public Vector2 boxSize;
    public LayerMask groundLayer;
    public float castDistance;

    private bool _jumpIntent;
    private bool _isJumping;
    private float _jumpBufferTimer;
    private float _coyoteTimer;

    public static bool IsBounce { set; get; }
    
    // for movement and animation
    private float _horizontal;
    private bool _isFacingRight = true;
    
    private float _idleTimer = 3f;
    
    private Animator _animator;
    
    // Hit Effect
    private Volume colorShader;
    private Bloom shaderBloom;
    private Volume hitVolume;
    private Bloom hitBloom;
    private bool isHitEffectActive;

    private void OnEnable()
    {
        PlayerInLighDetect.UserInTheLighDelegate += ReduceHealth;
        inputChannel.moveEvent += MoveHorizontal;
        inputChannel.jumpEvent += JumpInputBuffer;
    }

    private void OnDisable()
    {
        PlayerInLighDetect.UserInTheLighDelegate -= ReduceHealth;
        inputChannel.moveEvent -= MoveHorizontal;
        inputChannel.jumpEvent -= JumpInputBuffer;
    }

    private void Start()
    {
        healthChannel.QueryHealth();
        
        _stats = GetComponent<Stats>();
        _rb2d = GetComponent<Rigidbody2D>();
        _abilityController = GetComponent<AbilityController>();
        _animator = GetComponent<Animator>();

        colorShader = GameObject.FindGameObjectWithTag("levelShader").GetComponent<Volume>();
        colorShader.profile.TryGet(out shaderBloom);
        hitVolume = transform.GetComponentInChildren<Volume>();
        hitVolume.profile.TryGet<Bloom>(out hitBloom);
        ApplyRandomShader();
    }

    private void Update()
    {
        JumpTimers();
        PerformJump();
    }

    private void LateUpdate()
    {
        
        // Various checks for animations
        if (_rb2d.velocity.x != 0)
        {
            _animator.SetBool("IsIdle", false);
            _animator.SetBool("IsMoving", true);
            _idleTimer = 3f;
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
        if (_rb2d.velocity.y > 0)
        {
            _animator.SetBool("IsIdle", false);
            _animator.SetBool("InAir", true);
            _idleTimer = 3f;
        }
        else
        {
            _animator.SetBool("InAir", false);
            _animator.SetBool("IsJumping", false);
        }

        if (_rb2d.velocity.x == 0 && _rb2d.velocity.y == 0)
        {
            if (_idleTimer > 0)
                _idleTimer -= Time.deltaTime;
            else
                _animator.SetBool("IsIdle", true);
        }
    }

    private void FixedUpdate()
    {
        IsGrounded();
        Flip();
    }

    private void MoveHorizontal(float amount)
    {
        _horizontal = amount;
        
        var moveSpeed = _horizontal * _stats.CurrentMoveSpeed;

        _rb2d.velocity = new Vector2(moveSpeed, _rb2d.velocity.y);
    }

    private void JumpInputBuffer(bool jumpIntent)
    {
        if (jumpIntent)
        {
            _jumpBufferTimer = _stats.jumpBufferTime;
        }

        _jumpIntent = jumpIntent;
    }
    
    private void PerformJump()
    {
        if (_coyoteTimer > 0f && _jumpBufferTimer > 0f && !_isJumping)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _stats.CurrentJumpForce);
            // _rb2d.AddForce(new Vector2(_rb2d.velocity.x, _stats.CurrentJumpForce), ForceMode2D.Impulse);
            
            if (IsBounce)
            {
                _abilityController.GetAbilityVFX(Ability.AbilityTypes.BounceType).Play();
            }
            
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsJumping", true);

            _jumpBufferTimer = 0f;
            StartCoroutine(JumpCooldown());
        }

        if (_jumpIntent || !(_rb2d.velocity.y > 0f)) return;
        
        _rb2d.velocity = new Vector2(_rb2d.velocity.x,  -_rb2d.velocity.y * 0.1f);
        // _rb2d.AddForce(new Vector2(0, -_rb2d.velocity.y), ForceMode2D.Impulse);

        _coyoteTimer = 0f;
    }

    private void JumpTimers()
    {
        if (_coyoteTimer > 0)
        {
            _coyoteTimer -= Time.deltaTime;
        }
        
        if (_jumpBufferTimer > 0)
        {
            _jumpBufferTimer -= Time.deltaTime;
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
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            _coyoteTimer = _stats.coyoteTime;
            return true;
        }
        return false;
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

    void ReduceHealth()
    {
        healthChannel.ChangeHealth(_stats.lightDamage);
        
        if (!isHitEffectActive)
        {
            StartCoroutine(GetHitLightEffect());
        }
        if (healthChannel.playerHealth == 0)
        {
            DeathMenuManager.MenuManager.Death();
            Debug.LogWarning($"You are dead, Game Over!!");
        }
    }

    IEnumerator GetHitLightEffect()
    {
        isHitEffectActive = true;
        hitVolume.enabled = true;
        hitBloom.intensity.value = 8f;
        hitBloom.scatter.value = 0.400f;
        yield return new WaitForSeconds(0.2f);
        hitBloom.scatter.value = 0.125f;
        hitBloom.intensity.value = 0.05f;
        yield return new WaitForSeconds(0.2f);
        isHitEffectActive = false;
        hitVolume.enabled = false;
    }
    
    private void ApplyRandomShader()
    {
        colorShader.enabled = true;
        colorShader.weight = 0.5f;
        hitBloom.intensity.value = 2f;
        shaderBloom.tint.value = Random.ColorHSV();
    }
}
