﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehavior : MonoBehaviour
{
    public GuardMechanics guardMechanics;

    public GameObject individualGuard;
    public GameObject waypoint;
    public GameObject individualSpeaker;
    public float guardTimer;
    private bool guardIsWaiting = false;

    private void Start()
    {
        guardTimer = 0;
    }
    private void Update()
    {
        if (guardIsWaiting == true)
        {
            guardTimer = guardTimer + Time.deltaTime;
        }

        if (guardTimer >= 3)
        {
            guardIsWaiting = false;
            guardTimer = 0;
    //        Debug.Log("Timer Ended");
        }

    }
    void OnTriggerStay2D(Collider2D sp)
    {
        if (sp.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                individualGuard.SendMessage("StartMoveToWaypoint", waypoint);
                Debug.Log("What's That?");
            }
        }
        if (sp.gameObject.tag == "Guard")
        {
            guardIsWaiting = true;
        }
    }
}
