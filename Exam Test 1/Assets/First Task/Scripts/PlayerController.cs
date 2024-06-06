using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 700.0f;
    public float health = 100.0f;
    public float attackDamage = 10.0f;
    public float attackCooldown = 1.0f;
    public float attackRange = 3.0f;

    private float lastAttackTime;


    private Rigidbody rb;
    private Vector2 movementAxis;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
                EnemyController enemy = hitCollider.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
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
        // Trigger player bleeding system here
        Debug.Log("Player bleeding...");
        if (health <= 0)
        {
            // Player dies
            Destroy(gameObject);
            Debug.Log("Player died");
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
