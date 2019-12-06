using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemList : MonoBehaviour
{
    public PickupSystem pickup;
    public GameObject[] items;
    public int iteration = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pickup.hasSodaCan)
        {
            //items[iteration] = GameObject.
        }
    }
}
