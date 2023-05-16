using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProperties : MonoBehaviour
{
    public GameObject[] wallPrefabs;


    public enum WallStyle
    {
        Plain,
        Window
    };

    public WallStyle selectWallStyle = WallStyle.Plain;


    public void ReplacePrefab()
    {
        // Get the current transform information
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Transform parent = transform.parent;

        GameObject newWallPrefab = Instantiate(GetWallPrefab(selectWallStyle), position, rotation, parent);

        DestroyImmediate(gameObject);
    }


    private GameObject GetWallPrefab(WallStyle style)
    {
        int styleIndex = (int)style;
        if (styleIndex >= 0 && styleIndex < wallPrefabs.Length)
        {
            return wallPrefabs[styleIndex];
        }
        return null;
    }


}
