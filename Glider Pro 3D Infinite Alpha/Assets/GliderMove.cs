using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderMove : MonoBehaviour
{
    public Transform player;
    public Transform gliderModel;

    public float tiltScale = 10f;
    public float tiltSpeed = 10f;

    private CharacterController controller;
    private Rigidbody rb;

    public float moveSpeed = 10f;
    public float rotateSpeed = 90f;

    public float fallSpeed = 1f;
    public float riseSpeed = 3f;

    private Vector3 forwardMoveVector;
    private Vector3 _EulerAngleVelocity;

    public float verticalAcceleration;

    private bool isFalling = true;
    private Vector3 verticalSpeed;


    //public Transform[] raycastPositions;
    private float distanceFromGround;


    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SetVerticalSpeed();

        FindDistanceFromGround();

        TiltGlider();

        forwardMoveVector = transform.forward * Input.GetAxis("Vertical") * moveSpeed;
        float currentPlayerYRot = player.eulerAngles.y;

        float rotateAmount = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        Vector3 targetPlayerRotation = new Vector3(0, currentPlayerYRot += rotateAmount, 0);



        rb.rotation = Quaternion.Euler(targetPlayerRotation);

        rb.velocity = forwardMoveVector + verticalSpeed;

    }

    void FindDistanceFromGround()
    {
        RaycastHit hit;

        float castRadius = 0.2f;
        Vector3 castDirection = -Vector3.up;
        float castDist = 10f;

        if (Physics.SphereCast(transform.position, castRadius, castDirection, out hit, castDist))
        {
            distanceFromGround = hit.distance;
        }


    }



    void TiltGlider()
    {


        float tiltModifier = Mathf.Clamp(distanceFromGround - 0.2f, 0, 1);
        tiltModifier = Mathf.Clamp(distanceFromGround * 2, 0, 1);


        float tiltX = Input.GetAxis("Vertical") * tiltScale * tiltModifier;
        float tiltZ = Input.GetAxis("Horizontal") * -tiltScale * tiltModifier;

        Vector3 tiltVector = new Vector3(tiltX, player.transform.eulerAngles.y, tiltZ);

        Quaternion targetRotation = Quaternion.Euler(tiltVector);

        gliderModel.rotation = Quaternion.Slerp(gliderModel.rotation, targetRotation, Time.deltaTime * tiltSpeed);


    }



    void SetVerticalSpeed()
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

        if (!isFalling)
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
        if (!other.gameObject.CompareTag("Player"))
        {
            //KillGlider();
        }
    }



    void KillGlider()
    {
        Destroy(gameObject);
    }


}
