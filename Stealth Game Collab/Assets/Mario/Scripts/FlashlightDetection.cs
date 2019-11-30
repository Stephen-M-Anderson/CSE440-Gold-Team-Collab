using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDetection : MonoBehaviour
{
    public EnemyScript enemyScript;

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            enemyScript.inView = true;
            Debug.Log("HEY");
            
            if(enemyScript.speed < 3) 
            {
                enemyScript.speed = 2;
            }else if(enemyScript.speed < 4)
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
                enemyScript.speed--;
            }
        }
    }
}
