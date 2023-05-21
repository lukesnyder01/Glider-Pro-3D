using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int targetFrameRate = 144;

    public class Room
    {
        public Material roomMaterial;
        public bool isInteriorRoom;
        public int[,,] roomPosition;
    }


    public Dictionary<int[,,], Room> roomDict;


    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
