using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private HingeJoint2D hinge;
    [SerializeField]
    private GuardMechanics assignedGuard;
    private JointMotor2D motorCopy;
    private Quaternion origRot;
    private Transform origPos;
    private GameObject player;

    private float rotationSpeed;
    [SerializeField]
    private float rotationWaitTime;
    private float waitTime;
    private Quaternion minRot, maxRot;
    private float minLim, maxLim;
    public bool trackingPlayer;
    private bool guardSummoned = false;
    [SerializeField]
    private float guardCooldown;
    private float guardwait;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trackingPlayer = false;
        hinge = gameObject.GetComponent<HingeJoint2D>();
        origRot = gameObject.transform.rotation;
        origPos = gameObject.transform;
        motorCopy = hinge.motor;
        minLim = hinge.limits.min;
        maxLim = hinge.limits.max;
        waitTime = rotationWaitTime;
        guardwait = guardCooldown;
        rotationSpeed = motorCopy.motorSpeed;
        Debug.Log("Min = " + minLim + ", Max = " + maxLim);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!trackingPlayer)
        {
            if (hinge.jointAngle >= maxLim) // if we've rotated our maximum amount, rotate the other way 
            {
                if (waitTime <= 0)
                {
                    motorCopy.motorSpeed = 0 - rotationSpeed;
                    hinge.motor = motorCopy;
                    hinge.useMotor = true;
                }
                else
                {
                    hinge.useMotor = false;
                    waitTime -= Time.fixedDeltaTime;
                }
            }
            else if (hinge.jointAngle <= minLim)
            {
                if (waitTime <= 0)
                {
                    motorCopy.motorSpeed = rotationSpeed;
                    hinge.motor = motorCopy;
                    hinge.useMotor = true;
                }
                else
                {
                    hinge.useMotor = false;
                    waitTime -= Time.fixedDeltaTime;
                }
            }
            else if (waitTime <= 0)
            {
                waitTime = rotationWaitTime;
            }
        }
        else
        {
            Vector2 direction = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            if (hinge.jointAngle < maxLim && hinge.jointAngle > minLim)
            {
                transform.up = direction;
                if (guardSummoned == false)
                {
                    ClosestWaypoint cwcopy = player.GetComponent<ClosestWaypoint>();
                    assignedGuard.SendMessage("StartMoveToWaypoint", cwcopy.closestWaypoint);
                    guardSummoned = true;
                }
            }
        }
        if (guardSummoned == true)
        {
            if (guardwait <= 0)
            {
                guardSummoned = false;
                guardwait = guardCooldown;
            }
            else
            {
                guardwait -= Time.fixedDeltaTime;
            }
        }
    }
}