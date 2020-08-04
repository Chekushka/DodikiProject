using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerScript : MonoBehaviour
{
    [Header("Speeds")]
    public float speed;
    public float jumpForce;

    [Header("Ground Physics")]
    public Transform isGroundedChecker;
    public LayerMask groundLayer;
    public float checkGroundRadius;
    public float rememberGroundedFor;
    
    private Rigidbody2D _rigBody;
    private float _lastTimeGrounded;
    private bool _facingLeft = false; 
    private bool _isGrounded = false; 
    private Animator _animator;
    private MoveState _moveState;

    void Start()
    {
        _rigBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
   

    void Update()
    {
        CheckIfGrounded();
        Move();
        Jump();
        
        if (!_isGrounded && _rigBody.velocity.y < 0)
        {
            _animator.SetBool("IsFalling", true);
        }
        else
        {
            _animator.SetBool("IsFalling", false);
        }
    }

    private void Move()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        //float movement = joystick.Horizontal;
        var velocity = _rigBody.velocity;
        velocity = new Vector2 (movement * speed, velocity.y);
        _rigBody.velocity = velocity;
        _animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        
        if (movement < 0 && !_facingLeft)
            ReverseImage ();
        else if (movement > 0 && _facingLeft)
            ReverseImage ();
    }

    private void ReverseImage()
    {
        _facingLeft = !_facingLeft;
        var transform1 = _rigBody.transform;
        Vector2 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }

    private void Jump()
    {
       // float verticalMove = joystick.Vertical; 
       //if (verticalMove >= .5f && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
       
        if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || Time.time - _lastTimeGrounded <= rememberGroundedFor))
        {
            _animator.SetTrigger("IsJumping");
            _rigBody.velocity = new Vector2(_rigBody.velocity.x, jumpForce);
        }
        
    }
    private void CheckIfGrounded()
    {
        var colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            _isGrounded = true;
        }
        else
        {
            if (_isGrounded)
            {
                _lastTimeGrounded = Time.time;
            }    
            _isGrounded = false;
        }
    }
    enum MoveState
    {
        Idle,
        Walk,
        Jump
    }
}