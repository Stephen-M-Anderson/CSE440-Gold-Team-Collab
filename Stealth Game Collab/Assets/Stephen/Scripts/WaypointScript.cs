using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public int numberOfMapWaypoints;
    public GameObject[] waypoints;
    public GameObject[] adjacentWaypoints;
    private GameObject[] tempNodeArray;
    private float currentNodeDistance;
    private float closestNodeDistance;
    public int numberOfAdjacentNodes;

    private int i, j;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new GameObject[numberOfMapWaypoints];
        tempNodeArray = new GameObject[numberOfMapWaypoints];
        adjacentWaypoints = new GameObject[8];
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint"); // grabs all the waypoints on the map and sticks them in the array
        closestNodeDistance = 0;
        i = 0;
        
        foreach (GameObject node in waypoints) // iterates through the waypoints, each individual waypoint that it checks is called node
        {
            currentNodeDistance = Vector2.Distance(transform.position, node.transform.position);    // measures the distance between our main node (the one this script is attatched to)
                                                                                                    // and the node currently being iterated through
            
            if (node == this.gameObject)
                continue;
            if (closestNodeDistance == 0) // if this is the first node we've checked...
            {
                closestNodeDistance = currentNodeDistance;  // well then this node is technically the closest one so lets just put it in that temp array yea?
                tempNodeArray[i] = node;         // bam it's in there
                i++; // Do it yourself for-loop, baby!
            }

            else if (currentNodeDistance < 3f)
            {
                tempNodeArray[i] = node;
                i++;
                if (currentNodeDistance < closestNodeDistance) 
                    closestNodeDistance = currentNodeDistance;
            }
        }
                                                                                                    
        j = 0;  // index for the adjacentWaypoints array
        foreach (GameObject node in tempNodeArray) // now we go through the temp array and find which nodes are actually the closest
        {
            if (node == null)
                continue;
            currentNodeDistance = Vector2.Distance(transform.position, node.transform.position); // get distance from this node to the node in the array
            if (currentNodeDistance < 3f) // if that node is either closest or diagnoal, pop it in the array!
            {
                adjacentWaypoints[j] = node;
                j++;
            }
        }

        foreach (GameObject node in adjacentWaypoints)
        {
            if (node != null)
            {
                numberOfAdjacentNodes += 1;
            }
        }
        tempNodeArray = new GameObject[numberOfAdjacentNodes];
        for ( i = 0; i < numberOfAdjacentNodes; i++)
        {
            tempNodeArray[i] = adjacentWaypoints[i];
        }
        adjacentWaypoints = tempNodeArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
