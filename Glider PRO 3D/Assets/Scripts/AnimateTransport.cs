using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTransport : MonoBehaviour
{
    public Transform target;
    Transform glider;
    GliderController gliderController;

    bool animationPlaying = false;
    private float moveSpeed = 0.6f;
    private float rotationSpeed = 2f;

    void Start()
    {
        glider = GameObject.FindGameObjectWithTag("Player").transform;
        gliderController = glider.gameObject.GetComponent<GliderController>();
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveGliderToTransport());


        }
    }

    void StopAnimation()
    {

    }

    private IEnumerator MoveGliderToTransport()
    {
        gliderController.DisableControls();

        // We move the glider in two stages
        // First, we move the glider to the center of the trigger and match rotation

        Vector3 centerPosition = transform.position;
        Quaternion targetRotation = transform.rotation;

        while (Vector3.Distance(glider.position, centerPosition) > 0.01f)
        {
            glider.position = Vector3.MoveTowards(
                glider.position, 
                centerPosition,
                moveSpeed * Time.deltaTime);
            glider.rotation = Quaternion.Slerp(
                glider.rotation, 
                targetRotation,
                rotationSpeed * Time.deltaTime);

            yield return null;
        }

        glider.position = centerPosition;
        glider.rotation = targetRotation;

        // Second, we  move the glider to the target position

        while (Vector3.Distance(glider.position, target.position) > 0.01f)
        {
            glider.position = Vector3.MoveTowards(
                glider.position,
                target.position,
                moveSpeed * Time.deltaTime);

            yield return null;
        }

        gliderController.EnableControls();

        Debug.Log("Finished Moving");
    }



}
