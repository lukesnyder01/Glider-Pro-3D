using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderController : MonoBehaviour
{
    public Vector3 respawnPosition;
    public Quaternion respawnRotation;

    public Transform player;

    private Rigidbody rb;

    public float moveSpeed = 10f;
    public float rotateSpeed = 90f;

    private float fallSpeed = 0.5f;
    private float blowerSpeed = 1.1f;
    private float blowerAcceleration = 7f;

    private Vector3 forwardMoveVector;

    private float inputX;
    private float inputZ;

    private bool inBlower = false;

    private Vector3 externalForce;
    private Vector3 targetPlayerRotation;

    private Blower targetBlower;
    private Vector3 blowerDirection;

    public bool controlsEnabled = true;

    void Start()
    {
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Make Rigidbody Kinematic
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controlsEnabled = !controlsEnabled;
        }

        HandlePlayerInput();

        ApplyGravity();
        ApplyBlowerForce();
        SetForwardMoveVector();
        SetTargetRotation();

        // Update position and rotation manually
        transform.position += (forwardMoveVector + externalForce) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(targetPlayerRotation);
    }

    void ApplyGravity()
    {
        if (externalForce.y <= -fallSpeed)
        {
            externalForce.y = -fallSpeed;
        }
        else
        {
            externalForce.y -= fallSpeed * Time.deltaTime * blowerAcceleration;
        }
    }

    private void ApplyBlowerForce()
    {
        if (inBlower)
        {
            if (externalForce.magnitude >= blowerSpeed)
            {
                externalForce = externalForce.normalized * blowerSpeed;
            }
            else
            {
                externalForce += blowerSpeed * Time.deltaTime * blowerDirection * blowerAcceleration;
            }
        }
        else
        {
            // Gradually reduce the blower force if exiting the blower
            if (externalForce.magnitude > 0.1f)
            {
                externalForce = Vector3.Lerp(externalForce, Vector3.zero, Time.deltaTime * blowerAcceleration);
            }
        }
    }

    private void HandlePlayerInput()
    {
        if (controlsEnabled)
        {
            inputZ = Input.GetAxis("Vertical");
            inputX = Input.GetAxis("Horizontal");
        }
    }

    private void SetForwardMoveVector()
    {
        forwardMoveVector = transform.forward * inputZ * moveSpeed;
    }

    private void SetTargetRotation()
    {
        float currentPlayerYRot = transform.eulerAngles.y;
        float rotateAmount = inputX * rotateSpeed * Time.deltaTime;
        targetPlayerRotation = new Vector3(0, currentPlayerYRot + rotateAmount, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }

        if (other.gameObject.CompareTag("AirColumn"))
        {
            targetBlower = other.GetComponent<Blower>();
            inBlower = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AirColumn"))
        {
            if (targetBlower == null)
            {
                targetBlower = other.GetComponent<Blower>();
            }

            inBlower = true;
            blowerDirection = targetBlower.blowerDirection;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AirColumn"))
        {
            targetBlower = null;
            blowerDirection = Vector3.zero;
            inBlower = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            KillGlider();
        }
    }

    void KillGlider()
    {
        transform.position = respawnPosition;
        transform.rotation = respawnRotation;
        externalForce = Vector3.zero;
    }
}
