using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
public class PlayerController : MonoBehaviour
{
    private Stats _stats;
    private Rigidbody2D _rb2d;
    private AbilityController _abilityController;
    private DeathMenu _deathMenu;
    private CameraController _cameraController;
    
    Items _items;
    
    private TMP_Text _hpText;
    private TMP_Text _timerText;

    private float _time;


    // for jump
    public Vector2 boxSize;
    public LayerMask groundLayer;
    public float castDistance;
    
    private bool _isJumping;
    private float _jumpBufferTimer;
    private float _coyoteTimer;

    private float _idleTimer = 3f;
    
    public static bool IsBounce { set; get; }

    
    // for movement and animation
    private float _horizontal;
    private bool _isFacingRight = true;
    
    private Animator _animator;

    // keep the player between levels

    // Hit Effect
    private Volume colorShader;
    private Bloom shaderBloom;
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
        _abilityController = GetComponent<AbilityController>();
        _animator = GetComponent<Animator>();
        _hpText = GameObject.FindGameObjectWithTag("hpText").GetComponent<TMP_Text>();
        _timerText = GameObject.FindGameObjectWithTag("timerText").GetComponent<TMP_Text>();
        _cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        colorShader = GameObject.FindGameObjectWithTag("levelShader").GetComponent<Volume>();
        colorShader.profile.TryGet(out shaderBloom);
        hitVolume = transform.GetComponentInChildren<Volume>();
        hitVolume.profile.TryGet<Bloom>(out hitBloom);
        _hpText.text = "HP: " + (int)_stats.Hp;
        ApplyRandomShader();
    }

    private void Update()
    {
        JumpVertical();
        _time += Time.deltaTime;
        _timerText.text = TimeSpan.FromSeconds(_time).ToString(@"m\:ss\:ff");
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
        _horizontal = Input.GetAxisRaw("Horizontal");
        
        MoveHorizontal();
        Flip();
        ViewUp();
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
            if (IsBounce)
            {
                _abilityController.GetAbilityVFX(Ability.AbilityTypes.BounceType).Play();
            }
            _animator.SetTrigger("Jump");
            _animator.SetBool("IsJumping", true);
            
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

    private void ViewUp()
    {
        var up = Input.GetAxisRaw("Vertical");
        _cameraController.AddYAxis(up);
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
            _hpText.text = $"HP: {(int)_stats.Hp}";
            if (!isHitEffectActive)
            {
                StartCoroutine(GetHitLightEffect());
            }
        }
        else
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
        yield return new WaitForSeconds(0.5f);
        hitBloom.scatter.value = 0.125f;
        hitBloom.intensity.value = 0.05f;
        yield return new WaitForSeconds(0.5f);
        isHitEffectActive = false;
        hitVolume.enabled = false;
    }
    
    private void ApplyRandomShader()
    {
        colorShader.enabled = true;
        colorShader.weight = 0.5f;
        hitBloom.intensity.value = 0.05f;
        shaderBloom.tint.value = Random.ColorHSV();
    }
}
