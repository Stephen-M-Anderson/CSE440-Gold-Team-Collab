using System.Collections;
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

    public NicEnemyScript enemyScript;
    public float overLapSize = 0.5f;

    private Transform playerPosition;
    private bool isTrackingPlayer = false;
    private bool isOverlapped = false;
    private bool guardIsWaiting = false;
    public float guardTimer = 0.0f;
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        isOverlapped = Physics2D.OverlapCircle(camCenter.position, overLapSize, PlayerDetection); //Player detection
        guardIsWaiting = Physics2D.OverlapCircle(camCenter.position, overLapSize, GuardDetection);

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

        if (guardIsWaiting)
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

        if (isOverlapped)
        {
            Debug.Log("Overlapping");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses triggers to determine when the Camera will switch.
        if (collision.gameObject.tag == "Switch")
        {

            trackingSpeed = trackingSpeed * -1;

        }


        if (collision.gameObject.tag == "Player" && isOverlapped == false) //allows camera to spot player.
        {
            isTrackingPlayer = true;
            Debug.Log("Player spotted by camera");
            enemyScript.cameraSpotted = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isOverlapped == false) //allows camera to spot player.
        {
            isTrackingPlayer = true;
            Debug.Log("Player spotted by camera");
            enemyScript.cameraSpotted = true;
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
