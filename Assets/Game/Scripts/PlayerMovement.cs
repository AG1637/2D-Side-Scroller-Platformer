using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    private float horizontalInput;
    public bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        //changes the direction the player is facing
        if (horizontalInput > 0.01f) // Facing right
        {
            transform.localScale = new Vector3(4, 4, 4);
        }
        else if (horizontalInput < -0.01f) //Facing left
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded == true) //jumping
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
    }

}
