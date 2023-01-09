using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRoomType : MonoBehaviour
{
    public Material[] materials;
    public Renderer[] renderersToChange;


    void Awake()
    {
        var newMat = materials[Random.Range(0, materials.Length)];

        for (int i = 0; i < renderersToChange.Length; i++)
        {
            renderersToChange[i].material = newMat;
        }

    }

}
