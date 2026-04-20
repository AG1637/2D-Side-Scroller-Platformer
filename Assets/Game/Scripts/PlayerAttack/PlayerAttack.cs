using UnityEngine;
using UnityEngine.Audio;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject shootEffect;
    public AudioClip shootSound;
    public AudioSource audioSource;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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

        if (bulletPrefab == null || firePoint == null)
        {
            return;
        }

        Invoke("CreateBullet", 0.5f);
    }

    private void CreateBullet()
    {
        // Instantiate a new bullet at the firePoint position
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (shootEffect != null)
        {
            Instantiate(shootEffect, firePoint.position, Quaternion.identity);
        }

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}