using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderTilt : MonoBehaviour
{
    public Transform player;
    public Transform gliderModel;

    private float sideTiltScale = 35f;
    private float forwardTiltScale = 12f;
    private float tiltSpeed = 15f;

    private float distanceFromGround;


    void Update()
    {
        FindDistanceFromGround();
        TiltGlider();
    }


    private void FindDistanceFromGround()
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


    private void TiltGlider()
    {
        float tiltModifier = Mathf.Clamp(distanceFromGround - 0.2f, 0, 1);
        tiltModifier = Mathf.Clamp(distanceFromGround * 2, 0, 1);

        float tiltX = Input.GetAxis("Vertical") * forwardTiltScale * tiltModifier;
        float tiltZ = Input.GetAxis("Horizontal") * -sideTiltScale * tiltModifier;

        Vector3 tiltVector = new Vector3(tiltX, player.transform.eulerAngles.y, tiltZ);

        Quaternion targetRotation = Quaternion.Euler(tiltVector);

        gliderModel.rotation = Quaternion.Slerp(gliderModel.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

}
