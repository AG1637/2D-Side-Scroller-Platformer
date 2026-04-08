using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float speed = 30;
    private bool hit;
    public float lifetime;
    public float force = 20f;
    public float damage;
    private float spawnTime;

    [Header("VFX")]
    public GameObject hitEffectPrefab;

    private BoxCollider boxCollider;
    private Rigidbody rb;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;

        rb.useGravity = false;
        Vector3 shootDirection = PlayerMovement.instance.movingLeft ? Vector3.left : Vector3.right;
        rb.linearVelocity = shootDirection * force;
    }
    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 3)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            return;
        }

        if (collision.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
        Destroy(gameObject);
    }
}