using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashlightScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main Menu"); //loads the Main Menu scene. Simple as that.   
        }
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