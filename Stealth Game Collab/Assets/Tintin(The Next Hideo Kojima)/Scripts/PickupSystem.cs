using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public bool hasBodySpray;
    public bool hasSodaCan;
    public bool hasMustacheComb;


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
            //Insert code of adding the Body Spray to your inventory here!
            if(collision.gameObject.name == "SodaCan")
            {
                hasSodaCan = true;
            }
            else if (collision.gameObject.name == "BodySpray")
            {
                hasBodySpray = true;
            }
            else if (collision.gameObject.name == "MustacheComb")
            {
                hasMustacheComb = true;
            }

            Destroy(gameObject);
        }
    }

}
