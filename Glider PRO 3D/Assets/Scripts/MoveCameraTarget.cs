using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTarget : MonoBehaviour
{
    public Transform cameraIdealPosition;
    public Transform cameraTarget;

    [SerializeField]
    private float targetMoveSpeed = 10f;

    private float castDist = 1f;
    private float castRadius = 0.1f;
    private Vector3 castDirection;

    private Vector3 hitPosition;


    void Awake()
    {
        cameraTarget.position = cameraIdealPosition.position;
        castDist = Vector3.Distance(transform.position, cameraIdealPosition.position);
    }


    void Update()
    {
        castDirection = (cameraIdealPosition.position - transform.position).normalized;

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, castRadius, castDirection, out hit, castDist))
        {
            hitPosition = hit.point - (castDirection * (castRadius / 2));
        }
        else
        {
            hitPosition = cameraIdealPosition.position;
        }

        cameraTarget.position = Vector3.Lerp(cameraTarget.position, hitPosition, Time.deltaTime * targetMoveSpeed);
    }

}
