using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public GameObject fuckingplayer;



    public Transform[] routes;
    private int randomRoute;


    void Start()
    {
        waitTime = startWaitTime;
        randomRoute = Random.Range(0, routes.Length);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, routes[randomRoute].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, routes[randomRoute].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomRoute = Random.Range(0, routes.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        Vector2 playerPosition = fuckingplayer.transform.position;
        Vector2 direction = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        direction = direction.normalized;
        transform.up = direction;
    }
}