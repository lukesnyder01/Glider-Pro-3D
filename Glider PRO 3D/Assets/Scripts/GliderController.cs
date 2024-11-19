using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderController : MonoBehaviour
{
    public Vector3 respawnPosition;
    public Quaternion respawnRotation;

    public Transform player;


    private CharacterController controller;
    private Rigidbody rb;

    public float moveSpeed = 10f;
    public float rotateSpeed = 90f;

    private float fallSpeed = 0.5f;
    private float riseSpeed = 1.1f;
    private float verticalAcceleration = 6f;

    private Vector3 forwardMoveVector;

    private float inputX;
    private float inputZ;

    private bool isFalling = true;

    private Vector3 verticalSpeed;
    private Vector3 targetPlayerRotation;


    void Start()
    {
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        HandlePlayerInput();
    }


    void FixedUpdate()
    {
        SetVerticalSpeed();

        SetTargetTranslation();

        SetTargetRotation();

        rb.rotation = Quaternion.Euler(targetPlayerRotation);
        rb.velocity = forwardMoveVector + verticalSpeed;
    }


    private void HandlePlayerInput()
    {
        inputZ = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
    }


    private void SetTargetTranslation()
    {
        forwardMoveVector = transform.forward * inputZ * moveSpeed;
    }


    private void SetTargetRotation()
    {
        float currentPlayerYRot = player.eulerAngles.y;
        float rotateAmount = inputX * rotateSpeed * Time.fixedDeltaTime;
        targetPlayerRotation = new Vector3(0, currentPlayerYRot += rotateAmount, 0);
    }


    private void SetVerticalSpeed()
    {
        if (isFalling)
        {
            if (verticalSpeed.y <= -fallSpeed)
            {
                verticalSpeed.y = -fallSpeed;
            }
            else
            {
                verticalSpeed.y -= fallSpeed * Time.deltaTime * verticalAcceleration;
            }
        }
        else if (!isFalling)
        {
            if (verticalSpeed.y >= riseSpeed)
            {
                verticalSpeed.y = riseSpeed;
            }
            else
            {
                verticalSpeed.y += riseSpeed * Time.deltaTime * verticalAcceleration;
            }
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
   
        if (other.gameObject.CompareTag("AirColumn"))
        {
            isFalling = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AirColumn"))
        {
            isFalling = false;
        }
    }




    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AirColumn"))
        {
            isFalling = true;
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
        rb.velocity = Vector3.zero;
        rb.rotation = respawnRotation;
        transform.position = respawnPosition;



        //Destroy(gameObject);
    }


}
