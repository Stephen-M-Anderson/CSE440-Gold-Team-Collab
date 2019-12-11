using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GuardMechanics : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public GameObject actualPlayer;

    public bool inView = false;

    public GameObject[] routeWaypoints;
    private Transform[] patrolRoute;
    public GameObject[] pathfindingNodes;
    public List<Transform> pathfindingPos;
    public List<Transform> pathfindingReturnPos;
    public bool isPathfinding;
    public bool isReturning;
    public bool isSearching;
    public bool isChasing;
    private int pathfindingIndex;
    private int randomRoute;
    private int index;

    public GameObject[] flashlightObject;
    public Light2D[] flashlightLight2D;

    // Stephen's Variables start here
    public ClosestWaypoint closestWaypoint;
    private WaypointScript wscopy;
    private GameObject lastKnownPlayerLocation;

    void Start()
    {
        GameObject[] templightarray = new GameObject[1];
        templightarray = GameObject.FindGameObjectsWithTag("Flashlight"); // grabs a copy of every flashlight in the scene
        Light2D templightobject;
        flashlightObject = new GameObject[3];
        flashlightLight2D = new Light2D[3];
        int j = 0;
        for (int i = 0; i < templightarray.Length; ++i)
        {
            if (Vector2.Distance(transform.position, templightarray[i].transform.position) < 1f) 
            {
                templightobject = templightarray[i].GetComponent<Light2D>();
                if (templightobject.pointLightOuterRadius == 7.5f) // change the float to be the outter radius of the lowest alert level flashlight
                {
                    flashlightObject[0] = templightarray[i];
                    flashlightLight2D[0] = templightobject;
                }
                else if (templightobject.pointLightOuterRadius == 8.5f) // change the float to be the outter radius of the medium alert level flashlight
                {
                    flashlightObject[1] = templightarray[i];
                    flashlightLight2D[1] = templightobject;
                }
                else if (templightobject.pointLightOuterRadius == 10f) // change the float to be the outter radius of the highest alert level flashlight
                {
                    flashlightObject[2] = templightarray[i];
                    flashlightLight2D[2] = templightobject;
                }
            }
        }
        flashlightObject[1].SetActive(false); // disables the two higher alert flashlights
        flashlightObject[2].SetActive(false);
        waitTime = startWaitTime;
        pathfindingIndex = 0;
        isPathfinding = false;
        isSearching = false;
        actualPlayer = GameObject.FindGameObjectWithTag("Player");
        closestWaypoint = gameObject.GetComponent<ClosestWaypoint>();
        wscopy = closestWaypoint.GetComponent<WaypointScript>();
        patrolRoute = new Transform[routeWaypoints.Length];

        index = 0;
        foreach (GameObject node in routeWaypoints)
        {
            patrolRoute[index] = node.transform;
            index++;
        }
        waitTime = startWaitTime;
    }

    private void Update()
    {
        wscopy = closestWaypoint.GetComponent<WaypointScript>();
        if (isPathfinding && !isSearching)
        {
            FollowNodePath();
        }
        else if (inView == true)
        {
            isChasing = true;
            Chase();
        }
        else if (inView == false && isChasing == true && isPathfinding == false)
        {
            isSearching = true;

        }
        else 
        {
            Patrol();
        }
    }

    public void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, actualPlayer.transform.position, speed * Time.deltaTime);
        Vector2 direction = new Vector2(actualPlayer.transform.position.x - transform.position.x, actualPlayer.transform.position.y - transform.position.y);
        direction = direction.normalized;
        transform.up = direction;
    }

    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolRoute[randomRoute].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolRoute[randomRoute].position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                randomRoute = Random.Range(0, patrolRoute.Length);
                waitTime = startWaitTime;

                transform.position = Vector2.MoveTowards(transform.position, patrolRoute[randomRoute].position, speed * Time.deltaTime);
                Vector2 direction = new Vector2(patrolRoute[randomRoute].position.x - transform.position.x, patrolRoute[randomRoute].position.y - transform.position.y);
                direction = direction.normalized;
                transform.up = direction;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else // this bit lets the guard walk back to their patrol route normally, instead of moonwalking
        {
            Vector2 direction = new Vector2(patrolRoute[randomRoute].position.x - transform.position.x, patrolRoute[randomRoute].position.y - transform.position.y);
            direction = direction.normalized;
            transform.up = direction;
        }
    }
    public void FollowNodePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, pathfindingPos[0].position, speed * Time.deltaTime); // head to the next node in the array
        if (Vector2.Distance(transform.position, pathfindingPos[0].position) < 0.1f) // if we're at the node, start going to the next one
        {
            pathfindingPos.RemoveAt(0);
        }
        if (pathfindingPos.Count == 0) // if we are at the target node, meaning the list of nodes to travel to is empty
        {
            if (isReturning == false) // when the guard reaches the point, they will path back to their starting point 
            {
                isReturning = true;
                StartMoveToWaypoint(routeWaypoints[0]);
            }
            else
            {
                isReturning = false;
                isPathfinding = false;
            }
        }
        else
        {
            Vector2 direction = new Vector2(pathfindingPos[0].position.x - transform.position.x, pathfindingPos[0].position.y - transform.position.y);
            direction = direction.normalized;
            transform.up = direction;
        }
    }

    public void GetNodePath(GameObject targetNode) // This is where the shortest path algorithm in WaypointScript is called
    {
        if (targetNode)
        {
            Debug.Log("Get Node Path TargetNode = " + targetNode.gameObject.name);
            wscopy.targetNode = targetNode; // tells the waypoints script what our target node is
            wscopy.SendMessage("StartShortestPath", this); // starts the pathfinding functions, tells the waypoint script who the guard is
            //Debug.Log("path nodes array = " + wscopy.pathNodesArray.ToString());
            pathfindingNodes = wscopy.pathNodesArray;
        }
    }

    public IEnumerator MoveToWaypoint(GameObject targetNode)
    {
        GetNodePath(targetNode); // the path that we have to follow is now stored in pathfindingNodes
        if (pathfindingNodes.Length == 0) 
        {
            yield return null; // keeps the execution paused here until the pathfinding algorithm is complete
        }
        //Debug.Log("Pathfinding Nodes = " + pathfindingNodes.ToString());
    }
    public void StartMoveToWaypoint(GameObject targetNode) // This is what other objects will message to send the guard off somewhere
    {
        StartCoroutine("MoveToWaypoint", targetNode);
    }
    private List<Transform> GetPathfindingPositions(GameObject[] origPath)
    {
        Transform[] positions = new Transform[origPath.Length];
        for (int i = 0; i < origPath.Length; ++i)
        {
            positions[i] = origPath[i].transform;
        }
        return positions.ToList();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Door")
        {
            DoorMechanics temp = col.gameObject.GetComponent<DoorMechanics>();
            if (temp.isOpen == false)
            {
                temp.SendMessage("GuardOpen");
            }
        }
    }

}
