using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rigBody;
   
    public float speed;
    public float jumpForce;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float rememberGroundedFor;
    float lastTimeGrounded;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
    }
   

    void Update()
    {
        CheckIfGrounded();
        Move();
        Jump();
        
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var moveBy = x * speed;
        rigBody.velocity = new Vector2(moveBy, rigBody.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
        }
    }


    void CheckIfGrounded()
    {
        var colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }
}
