using System.Collections;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 70.0f;
    private bool isGrounded;
    public float jumpForce = 5f;


    private Rigidbody rb;
    private Vector2 movementAxis;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Cube"))
        {
            float cubeHeight = collision.gameObject.transform.localScale.y;
            rb.AddForce(Vector3.up * cubeHeight * jumpForce, ForceMode.Impulse);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            isGrounded = false;
        }
    }



}
