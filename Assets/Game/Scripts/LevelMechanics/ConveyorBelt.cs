using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Vector3 conveyorDirection = Vector3.right; //direction (positive is right, negative is left)
    [SerializeField] private float conveyorSpeed = 5f;
    [SerializeField] private GameObject text;

    private BoxCollider boxCollider;
    private Rigidbody playerRb;
    private PlayerMovement playerMovement;
    private bool isPlayerOnBelt = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody>();
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
        if (conveyorDirection.x < 0)
        {
            text.transform.rotation = Quaternion.Euler(0, 180, 0); //arrows point left
        }
        else if (conveyorDirection.x > 0)
        {
            text.transform.rotation = Quaternion.Euler(0, 0, 0); //arrows point right
        }
        
        if (isPlayerOnBelt && playerRb != null && playerMovement != null) 
        { 
            Vector3 conveyorVelocity = conveyorDirection.normalized * conveyorSpeed;
            playerRb.linearVelocity = new Vector3(conveyorVelocity.x, playerRb.linearVelocity.y, conveyorVelocity.z);
        }
    }

    /*private void OnDrawGizmos()
    {
        if (!showDirection) return;

        BoxCollider col = GetComponent<BoxCollider>();
        if (col == null) return;

        Vector3 center = transform.position + col.center;
        Vector3 direction = conveyorDirection.normalized;
        Vector3 arrowStart = center;
        Vector3 arrowEnd = center + direction * 2f;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(arrowStart, arrowEnd);

        // Draw arrow head
        Gizmos.DrawLine(arrowEnd, arrowEnd - direction * 0.3f + Vector3.up * 0.2f);
        Gizmos.DrawLine(arrowEnd, arrowEnd - direction * 0.3f - Vector3.up * 0.2f);
    }*/
}