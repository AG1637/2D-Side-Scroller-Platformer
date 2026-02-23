using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public bool movingLeft;
    public bool onGround;
    public float speed;
    public float jumpPower;
    private float horizontalInput;
    //public float gravity;

    [Header("Wall Jumping")]
    public bool onWall;
    bool isSliding;
    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    bool wallJumping;

    private void Awake()
    {
        //References from gameobject
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //increaseGravity();
        movement();
        isOnWall(); //checks if player is touching wall and returns 'onwall' as true or false
        //Debug.Log("wall jump: " + wallJumping);
        //Debug.Log("issliding: " + isSliding);
        if (Input.GetKey(KeyCode.Space))
        {
            if (onGround == true)
            {
                Jump();
            }
            if (isSliding == true)
            {
                wallJumping = true;
                rb.linearVelocity = new Vector2(-horizontalInput * wallJumpForce.x, wallJumpForce.y);
                Invoke("StopWallJumping", wallJumpDuration);
            }

        }
        if(onWall && !onGround && horizontalInput != 0)
        {
            isSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isSliding = false;
        }
    }


    public void movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //changes the direction the player is facing
        if (horizontalInput > 0.01f) // Facing right
        {
            movingLeft = false;
            sprite.flipX = false;
        }
        else if (horizontalInput < -0.01f) //Facing left
        {
            movingLeft = true;
            sprite.flipX = true;
        }

        //used for changing animations between idle and running
        animator.SetBool("Running?", horizontalInput != 0);
        animator.SetBool("Grounded?", isGrounded());
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(0, jumpPower);
            rb.AddForce(Vector3.up * jumpPower);
            animator.SetTrigger("Jump");
        }
        else
        {
            if (horizontalInput == 0)
            {
                rb.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                rb.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
        }

    }

    void StopWallJumping()
    {
        wallJumping = false;
    }

    private bool isGrounded()
    {
        bool raycastHitGround = Physics.Raycast(boxCollider.bounds.center, Vector3.down, 3.8f, groundLayer);
        //Debug.Log(raycastHitGround);
        //Debug.DrawLine(boxCollider.bounds.center, boxCollider.bounds.center + Vector3.down * 3.8f);
        onGround = raycastHitGround;
        return raycastHitGround;
    }

    private bool isOnWall()
    {
        if (movingLeft == true)
        {
            bool raycastHitWallLeft = Physics.Raycast(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), Vector3.left, 3f, wallLayer);
            //Debug.DrawLine(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), boxCollider.bounds.center + Vector3.left * 3f);
            onWall = raycastHitWallLeft;
            return raycastHitWallLeft;
        }
        else
        {
            bool raycastHitWallRight = Physics.Raycast(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), Vector3.right, 3f, wallLayer);
            //Debug.DrawLine(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), boxCollider.bounds.center + Vector3.right * 3f);
            onWall = raycastHitWallRight;
            return raycastHitWallRight;

        }
    }

    /*private void increaseGravity()
    {
        Physics.gravity = new Vector3(0, -gravity, 0);
        while (!onGround)
        {
            gravity -= 1;
        }
    }*/
}
