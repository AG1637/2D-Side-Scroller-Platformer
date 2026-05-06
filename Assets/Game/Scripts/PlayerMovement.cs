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
    public float gravityScale = 10f;
    public float speed;
    private float horizontalInput;
    public bool canMove = true;

    [Header("Jumping")]
    public float jumpPower = 55f;
    public bool onGround;
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteCounter;
    [SerializeField] private int extraJumps = 1;
    private int jumpCounter;
    private bool hasJumped = false;
    private bool jumpInputPressed = false;

    private void Awake()
    {
        //References from gameobject
        instance = this;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        sprite = GetComponent<SpriteRenderer>();
        gravityScale = 10f;
        jumpPower = 55f;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //|| Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (coyoteCounter > 0 || jumpCounter > 0 || isGrounded())
            {
                jumpInputPressed = true;
                animator.SetTrigger("jump");
            }
        }
    }


    private void FixedUpdate()
    {
        if (canMove == true)
        {
            Vector3 customGravity = Physics.gravity * gravityScale;
            rb.AddForce(customGravity, ForceMode.Acceleration);
            movement();

            if (jumpInputPressed)
            {
                Jump();
                //Debug.Log("Jumped");
                jumpInputPressed = false;  // Consume the input
            }

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
                hasJumped = false; // Reset jump flag when grounded
            }
            else
                coyoteCounter -= Time.fixedDeltaTime; //Start decreasing coyote counter when not on the ground

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
        // Prevent jump spam - only jump once per key press
        if (hasJumped)
            return;

        // Check if we can jump
        if (coyoteCounter > 0) // Can jump from ground or coyote time
        {
            PerformJump();
            coyoteCounter = 0; // Use up coyote time
            hasJumped = true;
        }
        else if (jumpCounter > 0) // Can do extra jump
        {
            PerformJump();
            jumpCounter--;
            hasJumped = true;
        }
    }

    private void PerformJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        SoundManager.instance.PlaySound(jumpSound);
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
            if (hasShownExplanation == false)
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

