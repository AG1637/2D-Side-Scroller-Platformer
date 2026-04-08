using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform enemy, pointA, pointB;
    public float speed;
    public int health;
    Vector3 targetPoint;

    private void Start()
    {
        targetPoint = pointB.position;
    }

    private void Update()
    {
        if (Vector2.Distance(enemy.transform.position, pointA.position) < 0.05f)
        {
            targetPoint = pointB.position;
        }

        if (Vector2.Distance(enemy.transform.position, pointB.position) < 0.05f)
        {
            targetPoint = pointA.position;
        }

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPoint, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            GameManager.instance.playerHealth--;
        }
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
