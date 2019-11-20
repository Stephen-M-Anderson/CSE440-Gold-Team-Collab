using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehavior : MonoBehaviour
{
    public EnemyScript enemyScript;

    void OnTriggerStay2D(Collider2D sp)
    {
        if (sp.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
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
