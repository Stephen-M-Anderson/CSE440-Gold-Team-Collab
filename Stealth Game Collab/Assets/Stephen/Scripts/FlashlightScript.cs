using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D o)
    {
        Debug.Log("Entered!");
        Vector2 direction = new Vector2(transform.position.x - o.transform.position.x, transform.position.y - o.transform.position.y);
        Debug.DrawRay(transform.position, direction, Color.red, 1f);
        /* if (o.gameObject.tag == "Player")
         {
             guardMechanics.inView = true;
             Debug.Log("HEY");
             guardMechanics.speed = 3;
         }*/
    }
    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            Invoke("SetViewToFalse", 2f);
            Debug.Log("Must Have Been the Wind");
        }
    }
}