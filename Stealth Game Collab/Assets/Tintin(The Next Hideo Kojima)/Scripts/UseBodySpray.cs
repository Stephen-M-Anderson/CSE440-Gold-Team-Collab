using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBodySpray : MonoBehaviour
{
    public PickupSystem pickupsystemScript;
    public GameObject BodySpray;
    public Transform spawnPoint1;
    //public bool hasCan = true; //this bool tells us whether or not there is a can in your inventory
    private KeyCode item = KeyCode.Space;
    public bool usedSpray = false; //a bool to tell us whether we threw the can or not

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pickupsystemScript.hasBodySpray)
        {
            //Debug.Log("look ma, a sody pop");
            if (Input.GetKeyDown(item))
            {
                usedSpray = true;
                //Debug.Log("NUMBER 2");
                Instantiate(BodySpray, spawnPoint1.position, spawnPoint1.rotation);
                // FindObjectOfType<AudioManager>().Play("SodaThrow");
            }
            //no can(defend), if do right

        }
        else if (!pickupsystemScript.hasSodaCan)
        {
            Debug.Log("NUMBER 4");
            usedSpray = false;
        }
    }
}
