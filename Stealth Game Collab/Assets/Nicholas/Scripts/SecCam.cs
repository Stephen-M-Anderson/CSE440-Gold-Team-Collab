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
    public float detectionRadius = 0.5f;
    public float guardTimer = 0.0f;
    public bool isResumingRotation = false;
    


    public NicEnemyScript enemyScript;

    private Transform playerPosition;
    private bool isTrackingPlayer = false;
    public bool isOverlapped = false;
    private bool guardIsWaiting = false;
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
        else if(isTrackingPlayer == true && isResumingRotation == false)  //Tracks player when camera spots player
        {
            Vector2 direction = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
            direction = direction.normalized;
            transform.up = direction;
        }
        else if (isTrackingPlayer == false && isResumingRotation == true) //Camera will return to original rotation when camera stops spotting player.
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * 2); // Lerp computers and translates current rotation and start rotation
            resumeTimer += Time.deltaTime; // camera will spend some time resuming.
            if (resumeTimer >= 1)
            {
                resumeTimer = 0;
                isResumingRotation = false;
            }
        }

        if (guardIsWaiting)// Ai Camera Behavior.
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
        Debug.Log("Trigger detected.");

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
                    GetCaughtDetection temp = collision.gameObject.GetComponent<GetCaughtDetection>();
                    temp.SendMessage("YourCaught");
                    isTrackingPlayer = true;
                    isResumingRotation = false;
                    resumeTimer = 0;
                    enemyScript.cameraSpotted = true;
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
            if (isOverlapped == false)
            {
                isResumingRotation = true;
            }

        }
    }
}
