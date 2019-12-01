using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject[] pathNodesArray;
    public GameObject targetNode;


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
        for (i = 0; i < numberOfAdjacentNodes; i++)
        {
            tempNodeArray[i] = adjacentWaypoints[i];
        }
        adjacentWaypoints = tempNodeArray;

        neighboringIndicies = new int[adjacentWaypoints.Length];
        for (i = 0; i < neighboringIndicies.Length; i++) // finds the index for each of this waypoints adjactent waypoints
        {
            neighboringIndicies[i] = FindWaypointIndex(adjacentWaypoints[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator FindShortestPath(GuardMechanics Guard) // uses djikstra's algorithm to find the shortest path from this node to targetNode
    {
        GameObject currentNode;
        Stack<GameObject> pathNodes = new Stack<GameObject>();
        djlist = new List<GameObject>
        {
            Capacity = waypoints.Length
        };
        int[] visitedFrom = new int[waypoints.Length]; // lets us backtrack our path once we've found the path to the target
        int dji = 0; // dj index, used for indexing in the method
        djdistances = new float[waypoints.Length]; // array that stores the distance from our source node to each given node
        djvisited = new bool[waypoints.Length]; // keeps track of which nodes we've visited
        i = 0;
        //Debug.Log("Waypoints array size = " + waypoints.Length);
        //foreach (GameObject node in waypoints)
        for (i = 0; i < waypoints.Length; i++)
        {
            //Debug.Log("Loop #" + i + ", node name is " + waypoints[i].name);
            if (waypoints[i] == gameObject) // if this node is the source node, set distance to 0
            {
                djdistances[i] = 0;
            }
            else
            {
                djdistances[i] = 1f / 0; // apparently this makes it infinity? 
            }
            //Debug.Log("Set: djdistance [" + i + "] = " + djdistances[i]);
            djlist.Insert(i, waypoints[i]); // Copies waypoints array into djlist List, better functionality with lists!
        }
        while (djlist.Count > 0) // while the list is not empty
        {
            dji = FindShortestDistanceIndex(djdistances, djvisited); // find index for the node with shortest distance 
                                                                     // Debug.Log("DJ Index = " + dji + ", Node name = " + waypoints[dji].name);
            currentNode = waypoints[dji]; // copies the node we are currently checking the branches of

            if (djlist.Remove(currentNode)) // remove the node we are currently at from the list
            {
                //Debug.Log("Successfully removed the node from the list!");
            }
            else
            {
                //Debug.Log("Couldn't remove node from list! Oh no! Node = " + currentNode.name + " at index " + dji);
                break;
            }

            if (waypoints[dji] == targetNode) // we've arrived at the target node! Break the loop!
            {
                break; // More code probably needed here haha
            }

            nodeForDjikstra = currentNode.GetComponent<WaypointScript>(); // so we can access the adjacent waypoints of the node
            //foreach (GameObject neighbor in nodeForDjikstra.adjacentWaypoints) // goes through each of our current node's neighbors
            for (i = 0; i < nodeForDjikstra.numberOfAdjacentNodes; i++)
            {
                int neighborIndex = nodeForDjikstra.neighboringIndicies[i]; // grabs in index of the neighboring node in the waypoints array
                float alt = djdistances[dji] + Vector2.Distance(currentNode.transform.position, waypoints[neighborIndex].transform.position);
                //Debug.Log("N Loop: want to check " + nodeForDjikstra.adjacentWaypoints[i].name + ", currently checking neighbor: " + waypoints[neighborIndex].name + ", distance = " + alt);

                if (alt < djdistances[neighborIndex])
                {
                    //Debug.Log("N Loop: new shortest distance to " + waypoints[neighborIndex].name + ", index = " + neighborIndex + ", visitedFrom array size = " + visitedFrom.Length);
                    djdistances[neighborIndex] = alt;
                    visitedFrom[neighborIndex] = dji;
                }
            }
            djvisited[dji] = true;
            //Debug.Log("Set visited to " + djvisited[dji] + " at node " + waypoints[dji]);

            if (djlist.Count % 5 == 0) // this makes it so that we only check 8 nodes per frame to avoid freezing the game when we trigger the pathfinding. Change the mod value to change how many times we loop
            {
                yield return null;
            }
        }
        bool done = false;
        int targetIndex = FindWaypointIndex(targetNode);
        while (!done)
        {
            pathNodes.Push(waypoints[targetIndex]);
            //Debug.Log("Stack: Pushed " + waypoints[targetIndex] + ", new targetIndex = " + visitedFrom[targetIndex]);
            targetIndex = visitedFrom[targetIndex];
            if (waypoints[targetIndex] == gameObject)
            {
                done = true;
            }
        }
        Debug.Log("Shortest path alg. finished!");
        pathNodesArray = pathNodes.ToArray(); // put our path nodes stack into an array so we can iterate through it easier
        Transform[] tempPos = new Transform[pathNodesArray.Length]; // make a new array to extract the transform of each path node, saves work later
        for (i = 0; i < pathNodesArray.Length; i++) // copies the transform of the path nodes from pathNodesArray into tempPos
        {
            tempPos[i] = pathNodesArray[i].transform;
        }
        Guard.pathfindingPos = tempPos.ToList<Transform>();
        Guard.pathfindingNodes = pathNodesArray;
        Guard.isPathfinding = true;
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

    private int FindWaypointIndex(GameObject target)
    {
        int k = 0;
        for (k = 0; k < waypoints.Length; k++)
        {
            if (waypoints[k] == target)
                break;
        }
        return k;
    }
    public void StartShortestPath(GuardMechanics Guard)
    {
        StartCoroutine("FindShortestPath", Guard);
    }
}
