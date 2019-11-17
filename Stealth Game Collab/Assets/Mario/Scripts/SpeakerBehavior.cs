using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehavior : MonoBehaviour
{
    public EnemyScript enemyScript;

    void OnTriggerEnter2D()
    {
        if (gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("E"))
            {
                enemyScript.heardSpeaker = true;
                Debug.Log("What's That?");
            }
        }
    }
}
