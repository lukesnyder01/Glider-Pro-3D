using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    public Transform wallsAndFloors;
    public Material[] materials;

    public enum RoomStyle // your custom enumeration
    {
        Simple,
        Swingers
    };

    public RoomStyle selectRoomStyle = RoomStyle.Simple;



    public void ChangeRoomStyle()
    {
        var newMat = GetMaterialForRoomStyle(selectRoomStyle);

        foreach (Transform child in wallsAndFloors)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            renderer.material = newMat;
        }
    }


    private Material GetMaterialForRoomStyle(RoomStyle style)
    {
        int styleIndex = (int)style;
        if (styleIndex >= 0 && styleIndex < materials.Length)
        {
            return materials[styleIndex];
        }
        return null;
    }



}
