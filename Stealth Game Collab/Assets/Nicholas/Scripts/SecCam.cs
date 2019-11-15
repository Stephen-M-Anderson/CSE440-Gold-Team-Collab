using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecCam : MonoBehaviour
{
    public GameObject player; //Does nothing now, but will help Camera detect player and change to tracking
    public float trackingSpeed = 40.0f; //Speed of the Camera

    private bool firstRotation = true;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate((Vector3.forward * trackingSpeed * Time.deltaTime)); //rotates camera continuously
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses triggers to determine when the Camera will switch.
        if (collision.gameObject.tag == "Switch")
        {
            trackingSpeed = trackingSpeed * -1;

        }
        Debug.Log("Trigger detected.");

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player spotted by camera");
        }
    }
}
