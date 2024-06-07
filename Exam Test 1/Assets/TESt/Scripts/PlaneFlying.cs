using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneFlying : MonoBehaviour
{
    public float throttleIncrement = 0.1f;

    public float throattleMax = 200f;

    public float responsiveness = 10f;

    public ParticleSystem particles;

    public Transform tail; 
    public Transform wingLeft; 
    public Transform wingRight; 

    public float lift = 135f;
    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    Rigidbody rb;
    [SerializeField]
    TextMeshProUGUI throttleText;
    private float responseModifier
    {
        get { return (rb.mass / 10f) * responsiveness; }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))
        {
            throttle += throttleIncrement;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            throttle -= throttleIncrement;
        }
        throttle = Mathf.Clamp(throttle, 0, 100f);

    }
    private void Update()
    {
        HandleInputs();
        UpdateHud();
        if (throttle > 0)
        {
            if (!particles.isPlaying)
                particles.Play();
        }
        else
        {
            if (particles.isPlaying)
                particles.Stop();
        }
        UpdateControlSurfaces();
    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * throattleMax * throttle);
        rb.AddTorque(transform.up* yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(-transform.forward * roll * responseModifier);
        
        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
    }
    private void UpdateHud()
    {
        throttleText.text = "Throttle: " + throttle.ToString("F0") + "%\n";
        throttleText.text += "Airspeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n";
        throttleText.text += "Altitude: " + transform.position.y.ToString("F0") + "m";
    }
    private void UpdateControlSurfaces()
    {
        tail.localRotation = Quaternion.Euler(-90 + pitch * 15f, yaw * 15f, 0);

        
        wingLeft.localRotation = Quaternion.Euler(0, 0, roll * 10f);
        wingRight.localRotation = Quaternion.Euler(0, 0, -roll * 10f);
    }

}
