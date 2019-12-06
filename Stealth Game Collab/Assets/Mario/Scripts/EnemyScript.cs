using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public GameObject actualPlayer;
    public bool inView = false;
    public bool heardSpeaker = false;

    public SpeakerBehavior speakerBehavior;
    
    public Transform[] routes;
    private int randomRoute;

    private Transform playerPosition;
    private Transform speakerPosition;
    private Transform guardPosition;

    void Start()
    {
        waitTime = startWaitTime;
        randomRoute = Random.Range(0, routes.Length);
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speakerPosition = GameObject.FindGameObjectWithTag("Speaker").GetComponent<Transform>();
        guardPosition = GameObject.FindGameObjectWithTag("Guard").GetComponent<Transform>();
}

    private void Update()
    {
        if (inView == false && heardSpeaker == false)
        {
            patrol();
        }
        if (inView == true)
        {
            chase();
        }
        if (heardSpeaker == true && inView == false)
        {
            investigateSpeaker();
        }
    }

    public void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        Vector2 direction = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
        direction = direction.normalized;
        transform.up = direction;
    }

    public void patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, routes[randomRoute].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, routes[randomRoute].position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                randomRoute = Random.Range(0, routes.Length);
                waitTime = startWaitTime;

                transform.position = Vector2.MoveTowards(transform.position, routes[randomRoute].position, speed * Time.deltaTime);
                Vector2 direction = new Vector2(routes[randomRoute].position.x - transform.position.x, routes[randomRoute].position.y - transform.position.y);
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
        
        transform.position = Vector2.MoveTowards(speakerBehavior.individualGuard.transform.position, 
            speakerBehavior.individualSpeaker.transform.position, speed * Time.deltaTime);
        Vector2 direction = new Vector2(speakerBehavior.individualSpeaker.transform.position.x - transform.position.x, 
            speakerBehavior.individualSpeaker.transform.position.y - transform.position.y);
        direction = direction.normalized;
        transform.up = direction;
    }
}