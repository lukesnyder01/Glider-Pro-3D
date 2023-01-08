using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerStats: MonoBehaviour

{
    public static int score = 0;
    public static int livesRemaining = 2;

    public TMPro.TextMeshProUGUI gliderCountText;

    void Start()
    {
        gliderCountText.text = livesRemaining.ToString();
    }

    public void AddGlider()
    {
        livesRemaining += 1;
        gliderCountText.text = livesRemaining.ToString();
    }


}
