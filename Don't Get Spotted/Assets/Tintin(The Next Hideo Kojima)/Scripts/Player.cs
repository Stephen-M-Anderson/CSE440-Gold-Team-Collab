﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PauseMenu pausemenuscript;
    public Text saveconfirmation;
    

    
                                   // An attempt at loading player stuff on wake.
    private void Start()
    {
        GameObject immortal = GameObject.Find("Immortal Load Information");
        if (immortal)
        {
            LoadPlayer();
        }
    }
    

    public void SavePlayer()
    {
        Save.SavePlayer(this);
        saveconfirmation.text = "Saved!";
    }

    public void LoadPlayer()
    {
        PlayerData data = Save.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        pausemenuscript.Resume();
    }
}
