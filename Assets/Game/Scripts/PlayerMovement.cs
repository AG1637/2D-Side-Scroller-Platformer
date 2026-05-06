using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Rigidbody rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider boxCollider;

    public GameObject lifeLostText;
    public GameObject explanationText;
    private bool hasShownExplanation = false;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip collectibleSound;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private float footstepCooldown = 0.3f;
    public AudioClip loseLifeSound;
    private float footstepTimer = 0f;

    [Header("Movement")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public bool movingLeft;
    public float gravityScale = 7f;
    public float speed;
    private float horizontalInput;
    public bool canMove = true;

    [Header("Jumping")]
    public float jumpPower;
    public bool onGround;
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;
    [SerializeField] private int extraJumps;
    private int jumpCounter;

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
    private void FixedUpdate()
    {
        if (canMove == true)
        {
            Vector3 customGravity = Physics.gravity * gravityScale;
            rb.AddForce(customGravity, ForceMode.Acceleration);
            movement();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            //Adjustable jump height   
            if ((Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0) || (Input.GetKeyUp(KeyCode.W) && rb.linearVelocity.y > 0) || (Input.GetKeyUp(KeyCode.UpArrow) && rb.linearVelocity.y > 0))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
            }

            //rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
            
            if (horizontalInput != 0 && isGrounded())
            {
                PlayFootstepSound();
            }
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
        if (coyoteCounter <= 0 && jumpCounter <= 0)
        {
            return;
        }

        SoundManager.instance.PlaySound(jumpSound);

        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
        else
        {
            //if not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            else
            {
                if (jumpCounter > 0) //if we have extra jumps then jump and decrease the jump counter
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                    jumpCounter--;
                }
            }
        }

        coyoteCounter = 0;

    }

    private bool isGrounded()
    {
        bool raycastHitGround = Physics.Raycast(boxCollider.bounds.center, Vector3.down, 5.5f, groundLayer);
        //Debug.Log(raycastHitGround);
        //Debug.DrawLine(boxCollider.bounds.center, boxCollider.bounds.center + Vector3.down * 5.5f);
        onGround = raycastHitGround;
        return raycastHitGround;
    }

    public bool canAttack()
    {
        return isGrounded();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            SoundManager.instance.PlaySound(collectibleSound);
            GameManager.instance.coins++;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.playerHealth--;
            lifeLostText.SetActive(true);
            if(hasShownExplanation == false)
            {
                explanationText.SetActive(true);
                Invoke("HideText", 3);
                hasShownExplanation = true;
            }
            SoundManager.instance.PlaySound(loseLifeSound);
            Invoke("HideText", 3);
        }
    }

    private void PlayFootstepSound()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            SoundManager.instance.PlaySound(footstepSound);
            footstepTimer = footstepCooldown;
        }
    }

    public void HideText()
    {
        lifeLostText.SetActive(false);
        explanationText.SetActive(false);
    }
}
