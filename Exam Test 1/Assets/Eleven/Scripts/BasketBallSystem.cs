using UnityEngine;
using TMPro;

public class BasketballSystem : MonoBehaviour
{
    public Transform hoop;
    public GameObject ball;
    public TMP_Text scoreText;

    private float lastDistance;
    private const float maxDistance = 10f; // Max distance for three-point line

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hoop"))
        {
            CalculatePoints(collision.contacts[0].point);
        }
    }

    void CalculatePoints(Vector3 collisionPoint)
    {
        float distance = Vector3.Distance(collisionPoint, hoop.position);

        int points = 0;
        if (distance < maxDistance * 0.5f) // Close-range shot
        {
            points = 2;
        }
        else if (distance < maxDistance * 0.8f) // Mid-range shot
        {
            points = 3;
        }
        else // Three-point shot
        {
            points = 3;
        }

        // Display points
        scoreText.text = "Points: " + points.ToString();
    }
}
