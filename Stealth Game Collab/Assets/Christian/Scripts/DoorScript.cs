using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isOpenable; //This bool will be set to true when the player comes in contact with the trigger collider attatched to the door. This will determine whether or not the door can be opened by the player.
    public bool isOpen; //This bool will be set to true when the door has been opened. We will use this to see whether pressing 'E' when isOpenable is set to true will open or close the door.
    public KeyCode interact = KeyCode.E; //The key we'll use to interact with doors
    public GameObject Door;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        isOpenable = false; 
        isOpen = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() //I'm using Update rather than FixedUpdate because when I tried FixedUpdate, GetKeyDown was calling the interact key twice rather than once. And at that point what the fuck is the point of using GetKeyDown?
    {
        if (Input.GetKeyDown(interact))
        {
            Debug.Log("You pressed E");

            if (isOpenable == true && isOpen == false) //This opens the door
            {
                HingeJoint2D hinge = gameObject.GetComponent(typeof(HingeJoint2D)) as HingeJoint2D; //locally initializing our 2D Hinge Point
                rb.constraints = RigidbodyConstraints2D.None; //Removing the rotation constraints placed on the door originally
                hinge.enabled = true; //Enabling our HingeJoint2D
                isOpen = true; //Setting this bool to true tells us this door is open.
                Door.GetComponent<BoxCollider2D>().enabled = false;

            }
            else if (isOpenable == true && isOpen == true)
            {
                //HingeJoint2D hinge = gameObject.GetComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                //var motor = hinge.motor;
                //motor.motorSpeed = motor.motorSpeed * -1;
                isOpen = false;
                //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //This function and the one under it tell us whether or not the player is in contact with the door's trigger and the bool is set accordingly
    {
        if (collision.gameObject.tag == "Player")
        {
            isOpenable = true;
            Debug.Log("isOpenable = " + isOpenable);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isOpenable = false;
            Debug.Log("isOpenable = " + isOpenable);

            if (isOpen == true) //What this does is make it so that once the door has been opened it no longer has motor force applied to it. Before I added this to the script sometimes the player colliding with the opened door would shoot you across the map.
            {
                HingeJoint2D hinge = gameObject.GetComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                var motor = hinge.motor;
                motor.motorSpeed = 0;
            }
            
        }
    }
}

