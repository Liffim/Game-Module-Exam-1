using System.Collections;
using UnityEngine;

public class EnemyController8 : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float attackRange = 2.0f;
    public float attackCooldown = 1.0f;
    public float health = 50.0f;
    public Material enemyMaterial; // Reference to the enemy's material

    private float maxHealth;
    private float lastAttackTime;
    private Color startColor;
    private Material instantiatedMaterial; // Copy of the enemy's material

    private void Awake()
    {
        maxHealth = health;
        startColor = enemyMaterial.color;
        instantiatedMaterial = new Material(enemyMaterial); // Create a copy of the material
        GetComponent<Renderer>().material = instantiatedMaterial; // Assign the copy to the renderer
        UpdateMaterialIntensity();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (transform != null && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Attack();
        }
        else if (transform != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        if (player == null)
        {
            return;
        }

        if (Time.time - lastAttackTime > attackCooldown)
        {
            PlayerController8 playerController = player.GetComponent<PlayerController8>();
            if (playerController != null)
            {
                playerController.TakeDamage(10);
            }
            lastAttackTime = Time.time;
            Debug.Log("Enemy attacked");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateMaterialIntensity();
        Debug.Log("Enemy health: " + health);
        if (health <= 0)
        {
            // Enemy dies
            Destroy(gameObject);
            Debug.Log("Enemy died");
        }
    }

    private void UpdateMaterialIntensity()
    {
        if (instantiatedMaterial != null)
        {
            float intensity = health / maxHealth;
            instantiatedMaterial.color = startColor * intensity;
        }
    }
}
