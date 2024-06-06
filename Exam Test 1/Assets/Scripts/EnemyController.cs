using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float attackRange = 2.0f;
    public float attackCooldown = 1.0f;
    public float health = 50.0f;

    private float lastAttackTime;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Attack();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            player.GetComponent<PlayerController>().TakeDamage(10);
            lastAttackTime = Time.time;
            Debug.Log("Enemy attacked");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        // Trigger enemy bleeding system here
        Debug.Log("Enemy bleeding...");
        if (health <= 0)
        {
            // Enemy dies
            Destroy(gameObject);
            Debug.Log("Enemy died");
        }
        else
        {
            StartCoroutine(Bleed(1, 5, 1)); // Example bleeding: 1 damage every second for 5 seconds
        }
    }
    IEnumerator Bleed(float damage, float duration, float interval)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            health -= damage;
            Debug.Log("Enemy bleeding... Current health: " + health);
            if (health <= 0)
            {
                Debug.Log("Enemy died from bleeding");
                yield break;
            }
            elapsed += interval;
            yield return new WaitForSeconds(interval);
        }
    }

}
