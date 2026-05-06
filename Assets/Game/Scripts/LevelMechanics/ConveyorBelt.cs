using UnityEngine;

public class Conveyorbelt : MonoBehaviour
{
    [SerializeField] private Vector3 conveyorDirection = Vector3.right;
    [SerializeField] private float conveyorSpeed = 5f;
    [SerializeField] private GameObject arrows;

    private BoxCollider boxCollider;
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private bool isPlayerOnBelt = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            isPlayerOnBelt = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isPlayerOnBelt = false;
        }
    }

    private void FixedUpdate()
    {
        if (arrows != null)
        {
            if (conveyorDirection.x < 0)
            {
                arrows.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (conveyorDirection.x > 0)
            {
                arrows.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (isPlayerOnBelt && rb != null)
        {
            Vector3 conveyorForce = conveyorDirection.normalized * conveyorSpeed;
            rb.AddForce(conveyorForce, ForceMode.VelocityChange);
        }
    }
}