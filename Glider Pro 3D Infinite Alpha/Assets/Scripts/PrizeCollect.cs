using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizeCollect : MonoBehaviour
{
    public enum PrizeType
    { 
        glider,
        battery,
        bands,
        helium,
        clock,
    }

    public PrizeType _prizeType;

    private bool collided = false;

    private GameObject player;

    private PlayerStats playerStats;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && !collided)
        {
            Destroy(gameObject);

            switch (_prizeType)
            {
                case PrizeType.glider:
                    playerStats.AddGlider();
                    break;
                case PrizeType.battery:
                    break;
                case PrizeType.bands:
                    break;
                case PrizeType.helium:
                    break;
                case PrizeType.clock:
                    break;
            }

            collided = true;

        }
    }

}
