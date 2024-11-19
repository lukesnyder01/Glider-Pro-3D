using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBillboard : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        Vector3 dirToCamera = transform.position - mainCamera.position;
        dirToCamera = dirToCamera.normalized;
        dirToCamera.y = 0;
        transform.rotation = Quaternion.LookRotation(dirToCamera);



    }
}
