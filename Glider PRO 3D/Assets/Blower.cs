using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    public Vector3 blowerDirection;

    void Start()
    {
        blowerDirection = transform.forward;
        Debug.Log(blowerDirection);
    }


    void Update()
    {
        
    }
}
