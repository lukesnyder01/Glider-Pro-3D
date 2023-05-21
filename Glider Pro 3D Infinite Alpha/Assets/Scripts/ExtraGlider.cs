using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGlider : Prize
{

    public override void Collect()
    {
        if (collected)
        { 
            return; 
        }

        collected = true;

        playerStats.AddGlider();

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        //instatiate particle effect

        Destroy(gameObject);
    }

}
