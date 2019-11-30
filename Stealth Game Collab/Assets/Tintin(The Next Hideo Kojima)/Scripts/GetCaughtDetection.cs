using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCaughtDetection : MonoBehaviour
{
    public bool Sighted;
    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "EnemyLight")
        {
            Sighted = true;
            Debug.Log("HEY");
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "EnemyLight")
        {
            Sighted = false;
            Debug.Log("Must Have Been the Wind");
        }
    }
}
