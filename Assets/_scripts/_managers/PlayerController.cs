using System.Collections;
using Abilities;
using Infra.Channels;
using Infra.StatContainers;
using UI;
using UnityEngine;

namespace _managers
{
    [RequireComponent(typeof(Stats),typeof(PlayerItemInteractableManager))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField, Tooltip("Scriptable Object Channel to control Input")] 
        private PlayerInputChannel inputChannel;

    
        [Header("Stats Containers")] 
        [SerializeField, Tooltip("Scriptable Object Container for Player Move Speed")] 
        private MoveStatContainer playerMoveStats;
        [SerializeField, Tooltip("Scriptable Object Container for Player Jump Force")] 
        private JumpStatContainer playerJumpStats;

    
        private Stats _stats;
        private Rigidbody2D _rb2d;
        private AbilityController _abilityController;
        private DeathMenu _deathMenu;
    
        Items.Items _items;
    
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
        private bool _moveIntention;
        private float _moveIntendedAmount;
        private bool _isFacingRight = true;
    
        private float _idleTimer = 3f;
    
        private Animator _animator;

        private void OnEnable()
        {
            inputChannel.moveEvent += MoveIntention;
            inputChannel.jumpEvent += JumpInputBuffer;
        }

        private void OnDisable()
        {
            inputChannel.moveEvent -= MoveIntention;
            inputChannel.jumpEvent -= JumpInputBuffer;
        }

        private void Start()
        {
            _stats = GetComponent<Stats>();
            _rb2d = GetComponent<Rigidbody2D>();
            _abilityController = GetComponent<AbilityController>();
            _animator = GetComponent<Animator>();
            
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
            MoveHorizontal();
            Flip();
        }

        private void MoveHorizontal()
        {
            _horizontal = _moveIntendedAmount * playerMoveStats.CurrentMoveSpeed * Time.deltaTime;

            _rb2d.velocity = new Vector2(_horizontal, _rb2d.velocity.y);
        }

        private void MoveIntention(float amount)
        {
            // _moveIntention = amount != 0;
            _moveIntendedAmount = amount;
        }

        private void JumpInputBuffer(bool jumpIntent)
        {
            if (jumpIntent)
            {
                _jumpBufferTimer = playerJumpStats.jumpStats.jumpBufferTime;
            }

            _jumpIntent = jumpIntent;
        }
    
        private void PerformJump()
        {
            if (_coyoteTimer > 0f && _jumpBufferTimer > 0f && !_isJumping)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, playerJumpStats.CurrentJumpForce);
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
                _coyoteTimer = playerJumpStats.jumpStats.coyoteTime;
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
    }
}
