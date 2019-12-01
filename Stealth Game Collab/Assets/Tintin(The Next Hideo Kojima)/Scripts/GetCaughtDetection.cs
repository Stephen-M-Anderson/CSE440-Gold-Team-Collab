using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCaughtDetection : MonoBehaviour
{
    public bool Sighted;
    public Collider2D player;
    void OnTriggerStay2D(Collider2D o)
    {
        if (o.gameObject.tag == "EnemyLight")
        {
            Sighted = true;
            Debug.Log("Must have been Hey");
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
