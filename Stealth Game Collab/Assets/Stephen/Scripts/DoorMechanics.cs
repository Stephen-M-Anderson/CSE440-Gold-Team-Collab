using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanics : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 origPos;
    private Quaternion origRot;
    private GameObject[] guards;
    private GameObject closestGuard;
    private float guardDistance;
    public bool isOpen;
    private float tempDistance;
    public PlayerWalking player;
    public HingeJoint2D hinge;
    public float openingForce;
    public float doorOpenRange;
    public bool closing;
    private JointMotor2D mt;
    public float doorAngleDifference;
    public bool counterClockwise;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        origPos = transform.position;
        origRot = transform.rotation;
        mt.maxMotorTorque = 1000;
        if (counterClockwise)
        {
            openingForce *= -1;
        }
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        tempDistance = Vector2.Distance(transform.position, player.transform.position);
        doorAngleDifference = Quaternion.Angle(transform.rotation, origRot); // lets you see how open the door is in the inspector
        if (doorAngleDifference > 5)
        {
            isOpen = true;
        }
        if (Input.GetKeyDown(player.interact) && isOpen == false && (tempDistance < doorOpenRange || Vector2.Distance(player.transform.position, origPos) < doorOpenRange)) // if the door is closed and the player presses interact while in range, open the door
        { 
            rb.constraints = RigidbodyConstraints2D.None; // allows the door to move
            mt.motorSpeed = openingForce; // turns on the doors motor and gives it a force
            hinge.motor = mt;
            hinge.useMotor = true;
        }
        else if (isOpen == true)
        {
            if (Quaternion.Angle(transform.rotation, origRot) < 2) // if the door is open but very close to being shut, shut the door fully
            {
                closing = false; 
                isOpen = false;
                hinge.useMotor = false;
                transform.rotation = origRot; // reset the door to its shut position, aka it's original position
                transform.position = origPos;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else if (Input.GetKeyDown(player.interact) && tempDistance < doorOpenRange) // if the door is open and the player presses interact while in range, close the door
            {
                closing = true;
                hinge.useMotor = true;
                mt.motorSpeed = 0 - (openingForce / 2); // play around with this number to make it close faster or slower. 
                hinge.motor = mt;
                hinge.useMotor = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isOpen)
        {
            if (hinge.motor.motorSpeed < 1 && !closing) // if the door is opening but very slowly, turn the motor off. She's done boys.
            {
                hinge.useMotor = false;
            }
            else if (!closing) // if the door is opening after the player has pressed interact
            {
                mt.motorSpeed *= (0.8f); // rapidly slows the doors opening speed, gives it a "popped open" feel
                hinge.motor = mt;
            }
        }
    }
    public void GuardOpen() // method to allow a guard to open a door, accessed by sendMessage function.
    {
        if (isOpen == false)
        {
            rb.constraints = RigidbodyConstraints2D.None; // allows the door to move
            mt.motorSpeed = openingForce; // turns on the doors motor and gives it a force
            hinge.motor = mt;
            hinge.useMotor = true;
        }
    }
}
