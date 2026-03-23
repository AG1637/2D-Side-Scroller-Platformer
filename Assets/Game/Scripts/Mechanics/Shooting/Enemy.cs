using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    Vector3 targetPoint;

    private void Start()
    {
        targetPoint = pointB.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) < 0.05f)
        {
            targetPoint = pointB.position;
        }

        if (Vector2.Distance(transform.position, pointB.position) < 0.05f)
        {
            targetPoint = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            GameManager.instance.playerHealth--;
        }
    }
}
