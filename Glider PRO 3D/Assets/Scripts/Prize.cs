using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prize : MonoBehaviour, ICollectable
{
    public bool collected = false;

    public GameObject player;

    public PlayerStats playerStats;

    public AudioClip collectSound;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }


    public virtual void Collect()
    {

    }

}
