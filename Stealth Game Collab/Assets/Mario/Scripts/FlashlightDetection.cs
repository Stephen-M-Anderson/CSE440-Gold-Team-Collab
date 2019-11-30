using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDetection : MonoBehaviour
{
    public EnemyScript enemyScript;
    public float chaseTimer;
    public bool keepChasing;

    private void Start()
    {
        chaseTimer = 0;
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            enemyScript.inView = true;
            keepChasing = true;
            Debug.Log("HEY");

            if (enemyScript.speed < 3)
            {
                enemyScript.speed = 2;
            }
            if (enemyScript.speed < 4)
            {
                enemyScript.speed = 3;
            }
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            enemyScript.inView = false;
            Debug.Log("Must Have Been the Wind");
            

            if (enemyScript.speed >= 4)
            {
                enemyScript.speed = 1;
            }
        }
    }
}
