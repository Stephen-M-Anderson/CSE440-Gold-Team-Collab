using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehavior : MonoBehaviour
{
    public EnemyScript enemyScript;
    public float guardTimer;
    private bool guardIsWaiting = false;
    private void Start()
    {
        guardTimer = 0;
    }
    private void Update()
    {
        if (guardIsWaiting == true)
        {
            guardTimer = guardTimer + Time.deltaTime;
        }

        if (guardTimer >= 3)
        {
            enemyScript.heardSpeaker = false;
            guardIsWaiting = false;
            guardTimer = 0;
            Debug.Log("Timer Ended");
        }

    }
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
            guardIsWaiting = true;
        }
    }
}
