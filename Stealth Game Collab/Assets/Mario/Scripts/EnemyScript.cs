using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform playerTransform;

    bool chasing = false;
    bool waiting = false;
    private float target;
    public bool inView;

    Vector2 direction;
    private float speed = 2.0f;
    private int currentTarget;
    private Transform[] route = null;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Transform route1 = GameObject.Find("Point1").transform;
        Transform route2 = GameObject.Find("Point2").transform;
        route = new Transform[2] {
            route1,
            route2
        };

    }

    private void Update()
    {
        if (chasing)
        {
            direction = playerTransform.position - transform.position;
            rotateGuard();
        }

        if (!waiting)
        {
            transform.Translate(speed * direction * Time.deltaTime, Space.World);
        }

    }

    private void FixedUpdate()
    {
        target = Vector2.Distance(route[currentTarget].position, transform.position);

    }

    public void SetNextPoint()
    {
        int nextPoint = -1;

        do
        {
            nextPoint = Random.Range(0, route.Length - 1);
        }
        while (nextPoint == currentTarget);

        currentTarget = nextPoint;

        direction = route[currentTarget].position - transform.position;
        rotateGuard();
    }

    public void Chase()
    {
        direction = playerTransform.position - transform.position;
        rotateGuard();
    }

    public void StopChasing()
    {
        chasing = false;
    }

    private void rotateGuard()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector2(0, angle - 90));
        direction = direction.normalized;
    }

    public void StartChasing()
    {
        chasing = true;
    }


    public void ToggleWaiting()
    {
        waiting = !waiting;
    }
}
