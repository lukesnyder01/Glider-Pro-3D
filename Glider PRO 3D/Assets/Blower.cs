using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    public Vector3 blowerDirection;

    public bool targetBlower;
    public Transform targetTransform;

    void Start()
    {
        blowerDirection = transform.forward;
    }

    void OnTriggerStay(Collider other)
    {
        if (targetBlower)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var targetDir = targetTransform.position - other.transform.position;
                blowerDirection = targetDir.normalized;
            }
        }
    }
}
