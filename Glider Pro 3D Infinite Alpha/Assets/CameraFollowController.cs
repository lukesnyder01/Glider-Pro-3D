using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public float cameraRotationSpeed = 20f;
    public float cameraMoveSpeed = 10f;

    public Transform glider;

    public Transform cameraMoveTarget;

    public Quaternion currentRotation;

    private Rigidbody rb;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = transform.rotation;

        RotateCamera();

        MoveCamera();
    }


    private void RotateCamera()
    {
        Quaternion targetLookRotation = Quaternion.LookRotation(glider.position - transform.position);

        transform.rotation = Quaternion.Slerp(currentRotation, targetLookRotation, Time.deltaTime * cameraRotationSpeed);

    }


    private void MoveCamera()
    {
        //transform.position = cameraMoveTarget.position;
        transform.position = Vector3.Lerp(transform.position, cameraMoveTarget.position, Time.deltaTime * cameraMoveSpeed);
        //rb.MovePosition(Vector3.Lerp(transform.position, cameraMoveTarget.position, Time.deltaTime * cameraMoveSpeed));
    }


}
