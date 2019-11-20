﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecCam : MonoBehaviour
{
    public GameObject player; //Does nothing now, but will help Camera detect player and change to tracking
    public float trackingSpeed = 40.0f; //Speed of the Camera


    private Transform playerPosition;
    private bool isTrackingPlayer = false;
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (isTrackingPlayer == false)
        {
            transform.Rotate((Vector3.forward * trackingSpeed * Time.deltaTime)); //rotates camera continuously
        }
        else if(isTrackingPlayer == true)  //Tracks player when camera spots player
        {
            Vector2 direction = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
            direction = direction.normalized;
            transform.up = direction;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses triggers to determine when the Camera will switch.
        if (collision.gameObject.tag == "Switch")
        {

            trackingSpeed = trackingSpeed * -1;

        }
        Debug.Log("Trigger detected.");

        if (collision.gameObject.tag == "Player") //allows camera to spot player.
        {
            isTrackingPlayer = true;
            Debug.Log("Player spotted by camera");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")  //stops spotting player when player exits vision cone.
        {
            isTrackingPlayer = false;
        }
    }
}
