using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : MonoBehaviour
{
    public bool guardIsWaiting;
    public NicEnemyScript enemyScript;
    public GameObject patrollingGuard;
    public GameObject laserWall;
    public float guardTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (guardIsWaiting)
        {
            guardTimer += Time.deltaTime;
        }

        if (guardTimer >= 3)
        {
            guardTimer = 0;
            guardIsWaiting = false;
            enemyScript.laserTriggered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Alarm Sent");
            enemyScript.laserTriggered = true;
        }

        if (collision.gameObject.tag == "Guard")
        {
            guardIsWaiting = true;
        }
    }
}
