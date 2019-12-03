using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
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


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerItem")
        {
           // hasPlayerItem = true;
            Debug.Log("WOOP WOOP I FOUND AN ITEM");
            //Insert code of adding the Body Spray to your inventory here!
            if(collision.gameObject.name == "SodaCan")
            {
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
           // hasPlayerItem = false;
            Destroy(collision.collider.gameObject);
        }
    }

}
