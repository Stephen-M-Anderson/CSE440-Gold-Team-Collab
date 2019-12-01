using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTForPathing : MonoBehaviour
{
    public GameObject Guard;
    public PlayerWalking playerWalkingCopy;
    public ClosestWaypoint closestWaypointCopy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Guard.SendMessage("StartMoveToWaypoint", closestWaypointCopy.closestWaypoint);
        }
    }
}
