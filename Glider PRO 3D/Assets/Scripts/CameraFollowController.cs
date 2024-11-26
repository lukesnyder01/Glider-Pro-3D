using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public float cameraRotationSpeed = 20f;
    public float cameraMoveSpeed = 10f;
    public float freeLookSensitivity = 2f;

    public Transform player;
    public Transform cameraMoveTarget;

    private Quaternion currentRotation;
    private bool freeLook = false; // Toggle for free look mode
    private Vector2 rotationInput; // Stores mouse movement for free look

    void Update()
    {
        // Toggle free look mode
        if (Input.GetKeyDown(KeyCode.F))
        {
            freeLook = !freeLook;

            // Set the rotation input to match the current camera's rotation
            if (freeLook)
            {
                Vector3 currentEulerAngles = transform.rotation.eulerAngles;
                rotationInput.x = currentEulerAngles.y;
                rotationInput.y = currentEulerAngles.x;
            }
        }

        if (freeLook)
        {
            FreeLook();
        }
        else if (player != null)
        {
            LookAtPlayer();
            MoveCamera();
        }
    }

    private void LookAtPlayer()
    {
        // Smoothly rotates the camera to look towards the player
        Quaternion targetLookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, targetLookRotation, Time.deltaTime * cameraRotationSpeed);
    }

    private void MoveCamera()
    {
        // Smoothly moves the camera towards the move target
        transform.position = Vector3.Lerp(transform.position, cameraMoveTarget.position, Time.deltaTime * cameraMoveSpeed);
    }

    private void FreeLook()
    {
        // Capture mouse input
        rotationInput.x += Input.GetAxis("Mouse X") * freeLookSensitivity;
        rotationInput.y -= Input.GetAxis("Mouse Y") * freeLookSensitivity;

        // Clamp vertical rotation to prevent flipping
        rotationInput.y = Mathf.Clamp(rotationInput.y, -80f, 80f);

        // Apply rotation to the camera
        transform.rotation = Quaternion.Euler(rotationInput.y, rotationInput.x, 0);
    }
}
