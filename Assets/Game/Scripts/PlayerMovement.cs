using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public float speed;
    public float jumpPower;
    private float horizontalInput;
    private float wallJumpCooldown;


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
        horizontalInput = Input.GetAxis("Horizontal");
        //changes the direction the player is facing
        if (horizontalInput > 0.01f) // Facing right
        {
            sprite.flipX = false;
        }
        else if (horizontalInput < -0.01f) //Facing left
        {
            sprite.flipX = true;
        }

        //used for changing animations between idle and running
        animator.SetBool("Running?", horizontalInput != 0);
        animator.SetBool("Grounded?", isGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
            if (onWall() && !isGrounded())
            {
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                //rb.AddForce(Vector3.up * jumpPower);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }
    private void Jump()
    {
        //Debug.Log("Jump");
        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            rb.AddForce(Vector3.up * jumpPower);
            animator.SetTrigger("Jump");
        }
        /*else if (onWall() && !isGrounded())
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
            wallJumpCooldown = 0;
        }*/
    }
    private bool isGrounded()
    {
        bool raycastHit = Physics.Raycast(boxCollider.bounds.center, Vector3.down, 3.8f, groundLayer);
        //Debug.Log(raycastHit);
        //Debug.DrawLine(boxCollider.bounds.center, boxCollider.bounds.center + Vector3.down * 3.8f);
        return raycastHit;
    }
   

    private bool onWall()
    {
        //bool raycastHit = Physics.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector3(transform.localScale.x,0), 0.1f, wallLayer);
        return false;
    }

}
