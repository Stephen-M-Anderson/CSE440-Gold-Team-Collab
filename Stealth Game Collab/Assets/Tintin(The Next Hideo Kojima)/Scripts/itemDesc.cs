﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class itemDesc : MonoBehaviour
{
    public PickupSystem pickupsystem;
    public GameObject sodacantext;
    public GameObject bodysprayUI;
    public GameObject MustacheComb;
    public GameObject HUDpanel;
    //public GameObject itemdescWindow;
    public bool hasAlreadyPickedupSoda = false;
    public bool hasAlreadyPickedupBodySpray = false;
    public bool hasAlreadyPickedupComb = false;
    // Start is called before the first frame update
    void Start()
    {
       // sodacantext.SetActive(false);
        bodysprayUI.SetActive(false);
        //MustacheComb.SetActive(false);
        //itemdescWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pickupsystem.hasBodySpray && !hasAlreadyPickedupBodySpray)
        {
            bodysprayUI.SetActive(true);
            hasAlreadyPickedupBodySpray = true;
            HUDpanel.SetActive(false);    
        }
        else if (pickupsystem.hasSodaCan && !hasAlreadyPickedupSoda)
        {
            sodacantext.SetActive(true);
            hasAlreadyPickedupSoda = true;
            HUDpanel.SetActive(false);
        }
        else if (pickupsystem.hasMustacheComb && !hasAlreadyPickedupComb)
        {
            MustacheComb.SetActive(true);
            hasAlreadyPickedupComb = true;
            HUDpanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
        {
            sodacantext.SetActive(false);
            bodysprayUI.SetActive(false);
            HUDpanel.SetActive(true);
            MustacheComb.SetActive(false);
                //itemdescWindow.SetActive(false);
        }
        
    }
}
