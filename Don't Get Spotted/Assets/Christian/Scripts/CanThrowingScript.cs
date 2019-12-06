using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanThrowingScript : MonoBehaviour
{
    private Rigidbody2D rb; //This is the rigidbody component of the can.
    public bool notStopped = true; //this bool checks if the soda can has stopped moving

    public float canSpeed = 5.0f; //The spped of the can, no shit.
    public float maxThrowDistance = 5.0f; //The maximum distance we want a can to be able to be thrown. I made this public so it could easily be edited in the inspector.
    private float canDistance; //A float representing the distance between the initial positions of the can and the mouse

    private Vector3 canStart; //The location of the can when it spawns
    private Vector3 moveDirection; //The direction to move the can in. A vector that will later be defined as the starting position of the can minus the starting position of the mouse curser.
    private Vector3 mousePosition; //The location of the mouse curser represented in a Vector3

    //private KeyCode boolKey = KeyCode.O; //The key to activate the bool that would freeze the cans during testing

    // Start is called before the first frame update
    void Start()
    {

        Vector3 currentPosition = transform.position;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        moveDirection = mousePosition - currentPosition;
        moveDirection.z = 0;
        moveDirection.Normalize();

        canStart = transform.position;
        canDistance = Vector3.Distance(mousePosition, canStart);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(moveDirection * canSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y == 0 && notStopped) // if the can has stopped moving in the y axis
        {
            notStopped = false;  //has stopped
            FindObjectOfType<AudioManager>().Play("SodaThrow");
        }
        //These were for test purposes to make sure that I could ACTUALLY stop the cans under whatever conditions I wanted to set. 
        //It was convenient for localizing the problem so I'll keep it here in case I break this shit again.
        /*if (Input.GetKeyDown(boolKey))
        {
            if (freeze)
            {
                freeze = false;
            }
            else
            {
                freeze = true;
            }
        }

        if (freeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }*/

        Vector3 currentPositionUpdate = transform.position;
        float distanceTraveled = Vector3.Distance(currentPositionUpdate, canStart);

        //The idea is that I want the can to stop once the distance between where it is on update and where it began becomes larger or
        //equal to the distance between where it began and the mouse curser. My issue is that for some reason one of these values is
        //fucked somehow. 

        //Edit: It works now holy fuck its 4 am I am so happy please kill me.
        if (distanceTraveled >= canDistance) 
            {
                rb.velocity = Vector2.zero;
                //Debug.Log("I reached my max distance Soda Kun");
                
        }

        if (distanceTraveled > maxThrowDistance)
        {
            rb.velocity = Vector2.zero;
            //FindObjectOfType<AudioManager>().Play("SodaThrow");
        }

    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Insert code of adding the soda can to your inventory here!
            
            Destroy(gameObject);
        }
    } 
    */
}
