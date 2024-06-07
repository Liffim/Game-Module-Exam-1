using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public int maxBounces = 10; // Number of maximum bounces
    public float minSize = 0.5f; // Minimum size of the ball
    public float maxSize = 2.0f; // Maximum size of the ball
    public float minForce = 5.0f; // Minimum force applied on bounce
    public float maxForce = 10.0f; // Maximum force applied on bounce
    public Color[] colors; // Array of colors to choose from

    private int bounceCount = 0;
    private Rigidbody rb;
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialScale = transform.localScale;

        // Start the bouncing process
        Bounce();
    }

    void Bounce()
    {
        if (bounceCount >= maxBounces)
            return;

        // Randomly choose size, color, and force for the next bounce
        float newSize = Random.Range(minSize, maxSize);
        Color newColor = colors[Random.Range(0, colors.Length)];
        float newForce = Random.Range(minForce, maxForce);

        // Apply the chosen properties
        transform.localScale = initialScale * newSize;
        GetComponent<Renderer>().material.color = newColor;

        // Apply force in a random direction
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(direction * newForce, ForceMode.Impulse);

        bounceCount++;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Bounce
            Bounce();
        }
    }
}
