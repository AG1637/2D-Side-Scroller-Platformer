using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider boxCollider;
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    public bool movingLeft;
    public float gravityScale = 3f;
    public float speed;
    private float horizontalInput;

    [Header("Jumping")]
    public float jumpPower;
    public bool onGround;
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    public bool isOnWall;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    //private float wallJumpCooldown;

    private void Awake()
    {
        //References from gameobject
        instance = this;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        sprite = GetComponent<SpriteRenderer>();
        rb.useGravity = false;
    }
    private void Update()
    {
        Vector3 customGravity = Physics.gravity * gravityScale;
        rb.AddForce(customGravity, ForceMode.Acceleration);
        movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
        }

        if (onWall())
        {
            gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            gravityScale = 2.5f;
            rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
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
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0)
        {
            return;
        }

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            WallJump();
        }
        else
        {
            if (isGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            }
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        rb.AddForce(wallJumpForce);
        //wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        bool raycastHitGround = Physics.Raycast(boxCollider.bounds.center, Vector3.down, 3.8f, groundLayer);
        //Debug.Log(raycastHitGround);
        //Debug.DrawLine(boxCollider.bounds.center, boxCollider.bounds.center + Vector3.down * 3.8f);
        onGround = raycastHitGround;
        return raycastHitGround;
    }
    private bool onWall()
    {
        if (movingLeft == true)
        {
            bool raycastHitWallLeft = Physics.Raycast(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), Vector3.left, 3f, wallLayer);
            //Debug.DrawLine(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), boxCollider.bounds.center + Vector3.left * 3f);
            isOnWall = raycastHitWallLeft;
            return raycastHitWallLeft;
        }
        else
        {
            bool raycastHitWallRight = Physics.Raycast(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), Vector3.right, 3f, wallLayer);
            //Debug.DrawLine(boxCollider.bounds.center - new Vector3(0f, 2f, 0f), boxCollider.bounds.center + Vector3.right * 3f);
            isOnWall = raycastHitWallRight;
            return raycastHitWallRight;

        }
    }
    public bool canAttack()
    {
        return isGrounded() && !onWall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            GameManager.instance.coins ++;
        }
    }
}
