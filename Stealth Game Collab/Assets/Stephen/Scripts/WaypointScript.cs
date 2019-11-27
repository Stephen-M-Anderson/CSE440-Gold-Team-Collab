using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public int numberOfMapWaypoints;
    public GameObject[] waypoints;
    public GameObject[] adjacentWaypoints;
    public int[] neighboringIndicies;
    private GameObject[] tempNodeArray;
    private float currentNodeDistance;
    private float closestNodeDistance;
    public int numberOfAdjacentNodes;
    public bool connectsToDoor;
    public bool isDoor;
    public GameObject linkedDoor;

    private int i, j;

    // These variables are used for Djikstra's algorithm
    private float[] djdistances;
    private List<GameObject> djlist;
    private bool[] djvisited;
    public WaypointScript nodeForDjikstra;


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

        foreach (GameObject node in adjacentWaypoints) // this loops lets us know how many adjacent waypoints this waypoint connects to
        {
            if (node != null)
            {
                numberOfAdjacentNodes += 1;
            }
        }

        // This logic is used to link a waypoint to an adjacent doorway node. 
        if (connectsToDoor)
        {
            numberOfAdjacentNodes += 1;
        }
        tempNodeArray = new GameObject[numberOfAdjacentNodes];
        for ( i = 0; i < numberOfAdjacentNodes; i++)
        {
            tempNodeArray[i] = adjacentWaypoints[i];
        }
        adjacentWaypoints = tempNodeArray;

        neighboringIndicies = new int[adjacentWaypoints.Length];
        i = 0;
        foreach (GameObject node in adjacentWaypoints) // finds the index for each of this waypoints adjactent waypoints
        {
            neighboringIndicies[i] = FindWaypointIndex(node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public GameObject[] FindShortestPath(GameObject targetNode) // uses djikstra's algorithm to find the shortest path from this node to targetNode
    {
        // private float[] djdistances;
        // private Queue<GameObject> djq;
        // private bool[] djvisited;

        GameObject currentNode;
        int[] visitedFrom = new int[numberOfMapWaypoints]; // lets us backtrack our path once we've found the path to the target
        int dji = 0; // dj index, used for indexing in the method
        djdistances = new float[numberOfMapWaypoints]; // array that stores the distance from our source node to each given node
        djvisited = new bool[numberOfMapWaypoints]; // keeps track of which nodes we've visited
        i = 0;
        foreach (GameObject node in waypoints)
        {
            if (node == gameObject) // if this node is the source node, set distance to 0
            {
                djdistances[i] = 0;
            }
            else 
            {
                djdistances[i] = 1f / 0; // apparently this makes it infinity? 
            }
            Debug.Log("Set: djdistance [" + i + "] = " + djdistances[i]);
            djlist.Insert(i, node); // Copies waypoints array into djlist List, better functionality with lists!
            i++;
        }
        while (djlist.Count > 0) // while the list is not empty
        {
            dji = FindShortestDistanceIndex(djdistances, djvisited); // find index for the node with shortest distance 
            djlist.RemoveAt(dji); // remove the node we are currently at from the list

            currentNode = waypoints[dji]; // copies the node we are currently checking the branches of to see if we can't possibly 
            nodeForDjikstra = currentNode.GetComponent<WaypointScript>(); // so we can access the adjacent waypoints of the node

            if (waypoints[dji] == targetNode) // we've arrived at the target node! Break the loop!
            {
                break; // More code probably needed here haha
            }

            i = 0;
            foreach (GameObject neighbor in nodeForDjikstra.adjacentWaypoints) // goes through each of our current node's neighbors
            {
                int neighborIndex = neighboringIndicies[i]; // grabs in index of the neighboring node in the waypoints array
                float alt = djdistances[dji] + Vector2.Distance(currentNode.transform.position, neighbor.transform.position); 
                if (alt < djdistances[neighborIndex])
                {
                    djdistances[neighborIndex] = alt;
                    visitedFrom[neighborIndex] = dji;
                }
                i++;
            }
        }
    }
    private int FindShortestDistanceIndex(float[] dist, bool[] visited)
    {
        int k = 0;
        int result = 0;
        float min = 1f / 0;
        foreach (float d in dist)
        {
            if (d < min && visited[k] != true) // if the examined distance is shorter than min and hasn't been visited...
            {
                min = d; 
                result = k; // result = d's index in the dist array
            }
            k++;
        }
        return result;
    }
*/
    private int FindWaypointIndex(GameObject target)
    {
        int k = 0;
        foreach (GameObject node in waypoints)
        {
            if (node == target)
                break;
            k++;
        }
        return k;
    }
}
