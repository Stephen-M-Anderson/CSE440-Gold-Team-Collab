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

            if (isOpenable == true && isOpen == false)
            {
                HingeJoint2D hinge = gameObject.GetComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                rb.constraints = RigidbodyConstraints2D.None;
                hinge.enabled = true; //Enabling our HingeJoint2D
                isOpen = true;

            }
            else if (isOpenable == true && isOpen == true)
            {
                //HingeJoint2D hinge = gameObject.GetComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                //var motor = hinge.motor;
                //motor.motorSpeed = 200;
                isOpen = false;
                //rb.constraints = RigidbodyConstraints2D.FreezeAll;
                //hinge.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        }
    }
}

