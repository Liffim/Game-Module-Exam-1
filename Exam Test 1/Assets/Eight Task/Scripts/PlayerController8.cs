using System.Collections;
using UnityEngine;

public class PlayerController8 : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 700.0f;
    public float health = 100.0f;
    public float attackDamage = 10.0f;
    public float attackCooldown = 1.0f;
    public float attackRange = 3.0f;
    public Material playerMaterial;

    private float lastAttackTime;

    private float maxHealth;
    private Rigidbody rb;
    private Color startColor;
    private Vector2 movementAxis;
    private Material instantiatedMaterial;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maxHealth = health;
        startColor = playerMaterial.color;
        instantiatedMaterial = new Material(playerMaterial); // Create a copy of the material
        GetComponent<Renderer>().material = instantiatedMaterial;
        UpdateMaterialIntensity();
    }
    void Update()
    {
        UpdateMovementAxis();
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Player attacking...");
        lastAttackTime = Time.time;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit!");
                EnemyController8 enemy = hitCollider.GetComponent<EnemyController8>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.LogWarning("No EnemyController8 component found on the enemy.");
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy triggered!");
            EnemyController8 enemy = other.GetComponent<EnemyController8>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogWarning("No EnemyController8 component found on the enemy.");
            }
        }
    }
    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }
    private void UpdateMovementAxis()
    {
        movementAxis.x = Input.GetAxis("Horizontal");
        movementAxis.y = Input.GetAxis("Vertical");
    }
    private void UpdatePosition()
    {
        var position = transform.forward * (movementAxis.y * speed * Time.deltaTime);

        var currentPosition = rb.position;
        var newPosition = currentPosition + position;

        rb.MovePosition(newPosition);
    }
    private void UpdateRotation()
    {
        var rotationMovement = movementAxis.x * rotationSpeed * Time.deltaTime;
        var currentRotation = rb.rotation.eulerAngles;
        currentRotation.y += rotationMovement;

        var newRotation = Quaternion.Euler(currentRotation);
        rb.MoveRotation(newRotation);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Player bleeding...");
        UpdateMaterialIntensity();
        if (health <= 0)
        {
            // Player dies
            Destroy(gameObject);
            Debug.Log("Player died");
        }
        else
        {
            StartCoroutine(Bleed(1, 5, 1)); 
        }
    }
    IEnumerator Bleed(float damage, float duration, float interval)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            health -= damage;
            UpdateMaterialIntensity();
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
    private void UpdateMaterialIntensity()
    {
        if (playerMaterial != null)
        {
            float intensity = health / maxHealth;
            Color newColor = startColor * intensity;
            instantiatedMaterial.color = newColor; 
        }
    }

}
