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
    private bool isFreeLookMode = false; // Toggle for free look mode
    private Vector2 rotationInput; // Stores mouse movement for free look

    void Update()
    {
        // Toggle free look mode on key press (e.g., 'F')
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFreeLookMode = !isFreeLookMode;
        }

        if (isFreeLookMode)
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
        Quaternion targetLookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, targetLookRotation, Time.deltaTime * cameraRotationSpeed);
    }

    private void MoveCamera()
    {
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
