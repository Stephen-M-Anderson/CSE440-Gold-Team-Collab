using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCan : MonoBehaviour
{
    public PickupSystem pickupsystemScript;
    public GameObject Can;
    public Transform spawnPoint1;
    public bool hasCan = true; //this bool tells us whether or not there is a can in your inventory
    private KeyCode item = KeyCode.Space;
    public bool threwCan = false; //a bool to tell us whether we threw the can or not

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if (pickupsystemScript.hasSodaCan)
        {
            //Debug.Log("look ma, a sody pop");
            if (Input.GetKeyDown(item) && Time.timeScale == 1f)
            {
                threwCan = true;
                //Debug.Log("NUMBER 2");
                Instantiate(Can, spawnPoint1.position, spawnPoint1.rotation);
               // FindObjectOfType<AudioManager>().Play("SodaThrow");
            }
            //no can(defend), if do right
            
        }
        else if(!pickupsystemScript.hasSodaCan)
        {
            Debug.Log("NUMBER 4");
            threwCan = false;
        }
    }
}