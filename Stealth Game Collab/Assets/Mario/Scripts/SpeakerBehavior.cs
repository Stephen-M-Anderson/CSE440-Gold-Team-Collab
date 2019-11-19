using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehavior : MonoBehaviour
{
    public EnemyScript enemyScript;

    void OnTriggerEnter2D(Collider2D sp)
    {
        if (sp.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("e"))
            {
                enemyScript.heardSpeaker = true;
                Debug.Log("What's That?");
            }
        }
        if (sp.gameObject.tag == "Guard")
        {
            enemyScript.heardSpeaker = false;
        }
    }
}
