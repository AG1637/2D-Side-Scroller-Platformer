using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform enemy;
    public Transform pointA;
    public Transform pointB;
    public float speed;
    public float waitTime = 2f;    
    private bool isWaiting = false;
    Vector3 targetPoint;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Health")]
    public int maxHealth = 3;
    public int health;

    private void Start()
    {
        targetPoint = pointB.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = maxHealth;
    }

    private void Update()
    {      
        if (isWaiting) return;
        MoveToTarget();    
    }

    public void MoveToTarget()
    {
        animator.SetBool("isMoving", true);
        enemy.position = Vector3.MoveTowards(enemy.position,targetPoint,speed * Time.deltaTime);
        FlipSprite(targetPoint.x);

        if (Vector2.Distance(enemy.position, targetPoint) < 0.05f)
        {
            StartCoroutine(WaitBeforeMoving());
        }
    }

    public void FlipSprite(float targetX)
    {
        if (targetX > enemy.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        isWaiting = true;
        //Play idle animation
        animator.SetBool("isMoving", false);
        yield return new WaitForSeconds(waitTime);
        //Switch target
        if (targetPoint == pointA.position)
        {
            targetPoint = pointB.position;
        }
        else
        {
            targetPoint = pointA.position;
        }
        isWaiting = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.instance.enemiesKilled++;
        Destroy(gameObject);
    }
}
