using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestWaypoint : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject closestWaypoint;
    private WaypointScript wp;
    private GameObject tempWaypoint;
    public float distanceToWaypoint;
    private float tempDistance;
    private float closestNeighborDistance;

    // Start is called before the first frame update
    void Start()
    {
        tempWaypoint = GameObject.FindGameObjectWithTag("Waypoint"); // just grabs a waypoint from the scene to get at them sweet components
        wp = tempWaypoint.GetComponent<WaypointScript>();            // This script stores all the goodies we need
        distanceToWaypoint = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // First things first, we need to find the closest node first! On the first frame, closestWaypoint == null, so we go through
        // all of the waypoints in the map and figure out which one is closest using a simple distance comparison.
        if (distanceToWaypoint == -1) // this GameObject has not found its closest waypoint yet
        {
            foreach (GameObject node in wp.waypoints)
            {
                tempDistance = Vector2.Distance(rb.position, node.transform.position);
                if (distanceToWaypoint == -1 || distanceToWaypoint > tempDistance) // If this waypoint is the closest so far, make it official and put a gad damn ring on it
                {
                    distanceToWaypoint = tempDistance;
                    closestWaypoint = node;
                }
            }
            wp = closestWaypoint.GetComponent<WaypointScript>();
        }

        // Now that we've found the closest waypoint at the start, we don't need to check every single waypoint anymore!
        // Each waypoint stores an array of its neighboring waypoints. We just need to check if we're closer to any of those adjacent waypoints 
        // every frame! It keeps the math down in update and should prevent this script from grinding shit to a halt because of shitty code.
        // Not to say that this code isn't shitty in it's own right, but this is an attempt to make it less shitty.
        else
        {
            distanceToWaypoint = Vector2.Distance(rb.position, closestWaypoint.transform.position); // update the current distance to our currently set closest waypoint
            closestNeighborDistance = 100;
            foreach (GameObject neighbor in wp.adjacentWaypoints) // check neighboring waypoints
            {
                tempDistance = Vector2.Distance(rb.position, neighbor.transform.position); // find distance to neighbor                 
                if (tempDistance < distanceToWaypoint && tempDistance < closestNeighborDistance) // true if neighbor is closer than our current closest node
                {
                    tempWaypoint = neighbor;
                    closestNeighborDistance = tempDistance;
                }
            }
            if (closestNeighborDistance < distanceToWaypoint)
            {
                closestWaypoint = tempWaypoint;
                wp = closestWaypoint.GetComponent<WaypointScript>();
            }
        }
        if (distanceToWaypoint > 2) // if something breaks and the waypoint doesn't update correctly, recheck every node to find the closest one and unfuck things.
        {
            foreach (GameObject node in wp.waypoints)
            {
                tempDistance = Vector2.Distance(rb.position, node.transform.position);
                if (distanceToWaypoint > tempDistance) // If this waypoint is the closest so far, make it official and put a gad damn ring on it
                {
                    distanceToWaypoint = tempDistance;
                    closestWaypoint = node;
                }
            }
            wp = closestWaypoint.GetComponent<WaypointScript>();
        }
    }
}
