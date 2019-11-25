using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMechanics : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public GameObject actualPlayer;

    public bool inView = false;
    public bool heardSpeaker;

    public GameObject[] routeWaypoints;
    private Transform[] patrolRoute;
    private int randomRoute;
    private int index;

    private Transform playerPosition;
    private Transform speakerPosition;
    private Transform guardPosition;

    // Stephen's Variables start here
    public ClosestWaypoint closestWaypoint;

    void Start()
    {
        waitTime = startWaitTime;
        actualPlayer = GameObject.FindGameObjectWithTag("Player");
        closestWaypoint = gameObject.GetComponent<ClosestWaypoint>();
        patrolRoute = new Transform[routeWaypoints.Length];

        index = 0;
        foreach (GameObject node in routeWaypoints)
        {
            patrolRoute[index] = node.transform;
            index++;
        }

        waitTime = startWaitTime;
        /* Mario Code starts here
        waitTime = startWaitTime;
        randomRoute = Random.Range(0, patrolRoute.Length);
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speakerPosition = GameObject.FindGameObjectWithTag("Speaker").GetComponent<Transform>();
        guardPosition = GameObject.FindGameObjectWithTag("Guard").GetComponent<Transform>();
        */
    }

    private void Update()
    {
        if (inView == false && heardSpeaker == false)
        {
            Patrol();
        }
        if (inView == true)
        {
            Chase();
        }
        if (heardSpeaker == true && inView == false)
        {
            investigateSpeaker();
        }
    }

    public void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        Vector2 direction = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
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
    }

    public void investigateSpeaker()
    {
        Debug.Log("Found speaker");
        transform.position = Vector2.MoveTowards(transform.position, speakerPosition.position, speed * Time.deltaTime);
        Vector2 direction = new Vector2(speakerPosition.position.x - transform.position.x, speakerPosition.position.y - transform.position.y);
        direction = direction.normalized;
        transform.up = direction;
    }
}