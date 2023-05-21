using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public float cameraRotationSpeed = 20f;
    public float cameraMoveSpeed = 10f;

    public Transform player;
    public Transform cameraMoveTarget;

    private Quaternion currentRotation;


    void Update()
    {
        currentRotation = transform.rotation;

        if (player != null)
        {
            LookAtPlayer();

            MoveCamera();
        }

    }


    private void LookAtPlayer()
    {
        Quaternion targetLookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, targetLookRotation, Time.deltaTime * cameraRotationSpeed);
    }


    private void MoveCamera()
    {
        //transform.position = cameraMoveTarget.position;

        transform.position = Vector3.Lerp(transform.position, cameraMoveTarget.position, Time.deltaTime * cameraMoveSpeed);
    }
}
