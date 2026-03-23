using UnityEngine;
using UnityEngine.Audio;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown;
    public Transform firePoint;
    public ProjectilePool bulletPool;
    public GameObject shootEffect;
    public AudioClip shootSound;
    public AudioSource audioSource;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    public void Initialize(ProjectilePool pool)
    {
        bulletPool = pool;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        if (bulletPool == null)
        {
            bulletPool = FindFirstObjectByType<ProjectilePool>();
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.enemiesKilled++;
        }

        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        if (bulletPool == null || firePoint == null)
        {
            return;
        }
        var projGO = bulletPool.Spawn(firePoint.position, firePoint.rotation);
        if (shootEffect != null)
        {
            Instantiate(shootEffect, transform.position, Quaternion.identity);
        }
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
