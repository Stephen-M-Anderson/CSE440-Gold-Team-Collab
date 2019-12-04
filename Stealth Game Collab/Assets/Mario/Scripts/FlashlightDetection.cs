using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDetection : MonoBehaviour
{
    public GuardMechanics guardMechanics;

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            guardMechanics.inView = true;
            Debug.Log("HEY");
            guardMechanics.speed = 3;
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
           Invoke("SetViewToFalse", 2f);
           Debug.Log("Must Have Been the Wind");
           
        }
    }

    private void SetViewToFalse()
    {
        guardMechanics.inView = false;
        guardMechanics.speed = 2;
    }
}
