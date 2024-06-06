using System.Collections;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public float baseSpeed = 5.0f;       // Constant forward speed
    public float acceleration = 2.0f;    // Speed increase when pressing W
    public float deceleration = 2.0f;    // Speed decrease when pressing S
    public float rotationSpeed = 70.0f;  // Rotation speed
    public float jumpForce = 5f;         // Jump force
    private bool isGrounded;             // To check if the player is on the ground
    private Rigidbody rb;                // Rigidbody component
    private Vector2 movementAxis;        // Input axis for movement

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        UpdateMovementAxis();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdateMovementAxis()
    {
        movementAxis.x = Input.GetAxis("Horizontal");
        movementAxis.y = Input.GetAxis("Vertical");
    }

    private void UpdatePosition()
    {
        float forwardSpeed = baseSpeed;
        if (movementAxis.y > 0)
        {
            forwardSpeed += acceleration * movementAxis.y;
        }
        else if (movementAxis.y < 0)
        {
            forwardSpeed += deceleration * movementAxis.y;
        }

        Vector3 forwardMovement = transform.forward * (forwardSpeed * Time.deltaTime);
        Vector3 sideMovement = transform.right * (movementAxis.x * baseSpeed * Time.deltaTime);

        Vector3 movement = forwardMovement + sideMovement;
        Vector3 currentPosition = rb.position;
        Vector3 newPosition = currentPosition + movement;

        rb.MovePosition(newPosition);

        if (transform.position.z > 25)
        {
            transform.position = new Vector3(0, 1, -24);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Collision detected! Restarting...");
            transform.position = new Vector3(0, 1, -24);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
