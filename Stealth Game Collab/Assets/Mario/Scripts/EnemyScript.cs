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
    public bool heardSpeaker;

    public Transform[] routes;
    private int randomRoute;

    private Transform playerPosition;
    private Transform speakerPosition;

    void Start()
    {
        waitTime = startWaitTime;
        randomRoute = Random.Range(0, routes.Length);
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speakerPosition = GameObject.FindGameObjectWithTag("Speaker").GetComponent<Transform>();
    }

    private void Update()
    {
        if (inView == false)
        {
            patrol();
        }

        if (inView == true)
        {
            chase();
        }

        if (heardSpeaker == true)
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
        transform.position = Vector2.MoveTowards(transform.position, speakerPosition.position, speed * Time.deltaTime);
        Vector2 direction = new Vector2(speakerPosition.position.x - transform.position.x, speakerPosition.position.y - transform.position.y);
        direction = direction.normalized;
        transform.up = direction;
    }
}