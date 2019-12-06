using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCaughtDetection : MonoBehaviour
{
    public bool Sighted = false;
    public Collider2D player;
    int AlreadySighted = 0;


    public void YourCaught()
    {
        Sighted = true;
    }
    /*
    void OnTriggerStay2D(Collider2D o)
    {
        if (o.gameObject.tag == "EnemyLight")
        {
            
            Sighted = true;
            Debug.Log("Must have been Hey");
        }
    }
    
    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "EnemyLight")
        {
            if(AlreadySighted == 0)
            {
                FindObjectOfType<AudioManager>().Play("AlertMusic");
                FindObjectOfType<AudioManager>().Pause("RegularMusic");
                AlreadySighted++;
            }
            Debug.Log("Must have been Hey");
        }
    }

    */
    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "EnemyLight")
        {
            Sighted = false;
            Debug.Log("Must Have Been the Wind");
        }
    }
    
}
