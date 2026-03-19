using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prize : MonoBehaviour, ICollectable
{
    public bool collected = false;

    public PlayerStats playerStats;

    public AudioClip collectSound;


    void Start()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        Debug.Log(playerStats);
    }


    public virtual void Collect()
    {

    }

}
