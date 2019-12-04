using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public SpawnCan spawncanScript;
    public bool hasBodySpray;
    public bool hasSodaCan;
    public bool hasMustacheComb;
  //  public bool hasPlayerItem= false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(spawncanScript.threwCan)
        {
           // Debug.Log("NUMBER 3");
            hasSodaCan = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerItem")
        {
           // hasPlayerItem = true;
            //Debug.Log("WOOP WOOP I FOUND AN ITEM");
            //Insert code of adding the Body Spray to your inventory here!
            if(collision.gameObject.name == "Soda Can" && !spawncanScript.threwCan)
            {
                //Debug.Log("NUMBER 1");
                FindObjectOfType<AudioManager>().Play("SodaPickup");
                hasSodaCan = true;
            }
            else if (collision.gameObject.name == "Body Spray")
            {
                hasBodySpray = true;
            }
            else if (collision.gameObject.name == "MustacheComb")
            {
                hasMustacheComb = true;
            }
            else if (collision.gameObject.name == "Soda Can(Clone)" && !spawncanScript.threwCan)
            {  // There was an issue where the Instantiation of the SOda can named it Soda Can Clone 
                //so I made this statement to account for this
               // Debug.Log("NUMBER 1");
                FindObjectOfType<AudioManager>().Play("SodaPickup");
                hasSodaCan = true;
            }
            // hasPlayerItem = false;
            Destroy(collision.collider.gameObject);
        }
    }

}
