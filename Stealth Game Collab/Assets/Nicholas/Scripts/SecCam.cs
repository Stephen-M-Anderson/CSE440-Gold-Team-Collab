﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecCam : MonoBehaviour
{
    public Transform camCenter;
    public LayerMask PlayerDetection;
    public LayerMask GuardDetection;
    public float trackingSpeed = 40.0f; //Speed of the Camera
    public GameObject patrolGuard; //Guard that will respond to Camera.
    public GameObject secCamera;
    public float detectionRadius = 0.5f;
    public float guardTimer = 0.0f;

    public NicEnemyScript enemyScript;

    private Transform playerPosition;
    public bool isTrackingPlayer = false;
    public bool isOverlapped = false;
    private bool guardIsWaiting = false;
    public bool isResumingRotation = false;
    private Quaternion startRotation;
    private float resumeTimer = 0;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startRotation = transform.rotation;
    }

    void Update()
    {   
        isOverlapped = Physics2D.OverlapCircle(camCenter.position, detectionRadius, PlayerDetection); //Player detection
        guardIsWaiting = Physics2D.OverlapCircle(camCenter.position, detectionRadius, GuardDetection);


        if (isTrackingPlayer == false && isResumingRotation == false)
        {
            transform.Rotate((Vector3.forward * trackingSpeed * Time.deltaTime)); //rotates camera continuously
        }
        else if (isTrackingPlayer == true && isResumingRotation == false)  //Tracks player when camera spots player
        {
            Vector2 direction = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
            direction = direction.normalized;
            transform.up = direction;
        }
        else if (isTrackingPlayer == false && isResumingRotation == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * 2);
            resumeTimer += Time.deltaTime;
            if (resumeTimer >= 2)
            {
                resumeTimer = 0;
                isResumingRotation = false;
            }
        }
        if (guardIsWaiting) //adds delay between when guards arrives and when guard leaves.
        {
            guardTimer += Time.deltaTime;
        }
        else
        {
            guardTimer = 0;
        }
        if (guardTimer >= 6)
        {
            enemyScript.cameraSpotted = false;
            guardTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses triggers to determine when the Camera will switch.
        if (collision.gameObject.tag == "Switch" && isResumingRotation == false)
        {

            trackingSpeed = trackingSpeed * -1;

        }


        if (collision.gameObject.tag == "Player") //allows camera to spot player.
        {
            RaycastHit2D hit = Physics2D.Linecast(camCenter.position, playerPosition.position,
                LayerMask.GetMask("Player", "Default"));
            if (hit && isOverlapped == false)
            {
                Debug.Log("Hit " + hit.collider.name);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    isTrackingPlayer = true;
                    isResumingRotation = false;
                    enemyScript.cameraSpotted = true;
                    resumeTimer = 0;
                }
            }
#if false
                isTrackingPlayer = true;
                Debug.Log("Player spotted by camera");
                enemyScript.cameraSpotted = true;
#endif
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isOverlapped == false) //allows camera to spot player.
        {
            RaycastHit2D hit = Physics2D.Linecast(camCenter.position, playerPosition.position,
                LayerMask.GetMask("Player", "Default"));
            if (hit)
            {
                Debug.Log("Hit " + hit.collider.name);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    isTrackingPlayer = true;
                    isResumingRotation = false;
                    resumeTimer = 0;
                    enemyScript.cameraSpotted = true; //calls guard by flipping this bool
                }
            }
            else
            {
                isTrackingPlayer = false;
                enemyScript.cameraSpotted = true;

            }
#if false
            isTrackingPlayer = true;
            Debug.Log("Player spotted by camera");
            enemyScript.cameraSpotted = true;
#endif
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")  //stops spotting player when player exits vision cone.
        {
            isTrackingPlayer = false;
            isResumingRotation = true;
        }
    }
}
