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

    [Header("Wall Jumping")]
    public bool onWall;
    bool isSliding;
    public float wallSlidingSpeed;

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
        Movement();
        isOnWall(); //checks if player is touching wall and returns 'onwall' as true or false
        if (Input.GetKey(KeyCode.Space))
        {
            if (onGround == true)
            {
                Jump();
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


    public void Movement()
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
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
            };
        }
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
}
