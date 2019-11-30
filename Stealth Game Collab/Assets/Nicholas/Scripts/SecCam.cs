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
        isOverlapped = Physics2D.OverlapCircle(camCenter.position, 0.5f, PlayerDetection); //Player detection
        guardIsWaiting = Physics2D.OverlapCircle(camCenter.position, 0.5f, GuardDetection);

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses triggers to determine when the Camera will switch.
        if (collision.gameObject.tag == "Switch")
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
                    enemyScript.cameraSpotted = true;
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
        }
    }
}
