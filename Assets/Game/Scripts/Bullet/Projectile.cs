using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float speed = 30;
    private bool hit;
    public float lifetime;
    public float force;
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
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }

        lifetime += Time.deltaTime;
        if (PlayerMovement.instance.movingLeft == true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //rb.AddForce(transform.right * force);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            //rb.AddForce(transform.right * force);
        }
        if (lifetime > 5)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            return;
        }
        hit = true;
        boxCollider.enabled = false;
        Destroy(gameObject);
        if (collision.tag == "Enemy")
        {
            //collision.GetComponent<Health>()?.TakeDamage(1);
        }
    }
}