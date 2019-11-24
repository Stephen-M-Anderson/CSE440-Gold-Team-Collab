using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public int ArraySize;
    private GameObject[] waypoints;
    public GameObject[] adjacentWaypoints;
    private GameObject[] tempNodeArray;
    private float currentNodeDistance;
    private float closestNodeDistance;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint"); // grabs all the waypoints on the map and sticks them in the array
        int i = 0;
        closestNodeDistance = 0;
        foreach (GameObject node in waypoints) // iterates through the waypoints, each individual waypoint that it checks is called node
        {
            currentNodeDistance = Vector2.Distance(transform.position, node.transform.position);    // measures the distance between our main node (the one this script is attatched to)
                                                                                                    // and the node currently being iterated through
            if (closestNodeDistance == 0) // if this is the first node we've checked...
            {
                closestNodeDistance = currentNodeDistance;  // well then this node is technically the closest one so lets just put it in that temp array yea?
                tempNodeArray[i] = node;                    // bam it's in there
                i++; // Do it yourself for-loop, baby!
            }
            else if (currentNodeDistance <= closestNodeDistance) // if this node is the closest node that we've checked, or as far as the closest node...
            {
                closestNodeDistance = currentNodeDistance; // make it official, this one is the closest node
                tempNodeArray[i] = node; // pop that close ass node in that temp array
                i++; // Do it yourself for-loop, baby!
            }
        }
        float diagonalDistance = Mathf.Sqrt(closestNodeDistance * closestNodeDistance * 2); // some trig so we can connect nodes diagonally, uses rules for a 45-45-90 triangle
                                                                                            // A^2 + B^2 = C^2, A = B -> C = sqrt ( A * A * 2)

        foreach (GameObject node in tempNodeArray) // now we go through the temp array and find which nodes are closest
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
