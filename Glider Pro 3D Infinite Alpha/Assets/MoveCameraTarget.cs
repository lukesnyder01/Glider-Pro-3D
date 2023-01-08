using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTarget : MonoBehaviour
{

    public Transform cameraIdealPosition;
    public Transform cameraTarget;


    private Vector3 cameraTargetStartPosition;
    public float castDist = 1f;
    public float castRadius = 0.2f;
    private Vector3 castDirection;

    // Start is called before the first frame update
    void Start()
    {
        cameraTargetStartPosition = cameraIdealPosition.localPosition;
        cameraTarget.position = cameraTargetStartPosition;
        castDist = Vector3.Distance(transform.position, cameraIdealPosition.position);

        
    }

    // Update is called once per frame
    void Update()
    {
        castDirection = (cameraIdealPosition.position -transform.position).normalized;

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, castRadius, castDirection, out hit, castDist))
        {


            cameraTarget.position = hit.point - (castDirection * (castRadius/2));
        }
        else
        {
            cameraTarget.localPosition = cameraTargetStartPosition;
        }

        
    }
}
