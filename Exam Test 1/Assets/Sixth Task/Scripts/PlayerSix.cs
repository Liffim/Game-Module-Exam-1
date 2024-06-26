using System.Collections;
using UnityEngine;

public class PlayerSix : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 70.0f;
    public float jumpForce = 10f;
    private CubeGeneratorSix gridManager;


    private Rigidbody rb;
    private Vector2 movementAxis;
    private bool isGrounded;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        gridManager = FindObjectOfType<CubeGeneratorSix>();
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            isGrounded = true;
        }

    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
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

        Debug.Log(movementAxis.x);
        Debug.Log(movementAxis.y);
        if (movementAxis.x != 0 || movementAxis.y != 0)
        {
            gridManager.RandomizeHeights();
        }
    }
    private void UpdatePosition()
    {
        var position = transform.forward * (movementAxis.y * speed * Time.deltaTime);

        var currentPosition = rb.position;
        var newPosition = currentPosition + position;
        if (newPosition != position)
        {
            gridManager.RandomizeHeights();
        }

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
