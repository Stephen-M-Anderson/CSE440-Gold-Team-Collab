using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDetection : MonoBehaviour
{
    public EnemyScript enemyScript;
    public float chaseTimer;

    private void Start()
    {
        chaseTimer = 30;
    }

    private void Update()
    {
        if (enemyScript.inView == true)
        {
            chaseTimer--;
        }
    }
    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            enemyScript.inView = true;
            Debug.Log("HEY");
            enemyScript.speed = 3;
            chaseTimer = 30;
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player" && chaseTimer > 0)
        {
            enemyScript.inView = false;
            Debug.Log("Must Have Been the Wind");
            enemyScript.speed = 2;
        }
    }
}
