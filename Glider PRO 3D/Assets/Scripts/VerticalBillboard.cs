using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBillboard : MonoBehaviour
{
    private Vector3 originalRotation;

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = originalRotation.y;
        transform.rotation = Quaternion.Euler(rotation);
    }


}
