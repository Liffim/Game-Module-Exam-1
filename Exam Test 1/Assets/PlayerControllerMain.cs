using System.Collections;
using UnityEngine;

public class PlayerControllerMain : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 70.0f;
    public float jumpForce = 10f;


    private Rigidbody rb;
    private Vector2 movementAxis;
    private bool isGrounded;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }
    void Update()
    {
        UpdateMovementAxis();
        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
    }
    void Jump()
    {
        // Add force upwards for jumping
        rb.velocity = Vector3.up * jumpForce;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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

    

}
