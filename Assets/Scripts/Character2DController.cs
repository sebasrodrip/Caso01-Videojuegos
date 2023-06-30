using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character2DController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 300.0F;
    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 140.0F;
    [SerializeField]
    float jumpGraceTime = 0.20F;
    [SerializeField]
    float fallMultiplier = 3.0F;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundMask;

    [Header("Extras")]
    [SerializeField]
    Animator animator;

    Rigidbody2D _rb;
    float _inputX;
    float _gravityY;
    float _lastTimeJumpPressed;
    bool _isMoving;
    bool _isJumpPressed;
    bool _isJumping;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravityY = - Physics2D.gravity.y;
    }

    void Update()
    {
        HandleInputs(); 
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleMove();
        HandleFlipX();
    }

    void HandleJump()
    {
        if (_lastTimeJumpPressed > 0.0F && Time.time - _lastTimeJumpPressed <= jumpGraceTime)
        {
            _isJumpPressed = true;
        }
        else
        {
            _lastTimeJumpPressed = 0.0F;
        }

        if (_isJumpPressed)
        {
            bool isGrounded = IsGrounded();

            if (isGrounded)
            {
                _rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            }
        }

        if (_rb.velocity.y < -0.01F)
        {
            _rb.velocity -= Vector2.up * _gravityY * fallMultiplier * Time.fixedDeltaTime;
        }

        _isJumping = !IsGrounded();
    }

    void HandleFlipX()
    {
        if (!_isMoving)
        {
            return;
        }
        bool facingRight = _inputX > 0.0F;

        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            transform.Rotate(0.0F, 180.0F, 0.0F);
        }
    }

    void HandleMove()
    {
        bool isMoving = animator.GetFloat("speed") > 0.01F;
        if (isMoving != _isMoving && !_isJumping)
        {
            animator.SetFloat("speed", Mathf.Abs(_inputX));
        }

        bool isJumping = animator.GetBool("isJumping");
        if (isJumping != _isJumping)
        {
            animator.SetBool("isJumping", _isJumping);
        }

        float velocityX = _inputX * moveSpeed * Time.fixedDeltaTime;

        Vector2 direction = new Vector2(velocityX, _rb.velocity.y);

        _rb.velocity = direction;
    }

    void HandleInputs()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _isMoving = _inputX != 0.0F;

        _isJumpPressed = Input.GetButtonDown("Jump");

        if(_isJumpPressed )
        { 
            _lastTimeJumpPressed = Time.time;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.63F, 0.4F), CapsuleDirection2D.Horizontal, 0.0F, groundMask);
    }

}
