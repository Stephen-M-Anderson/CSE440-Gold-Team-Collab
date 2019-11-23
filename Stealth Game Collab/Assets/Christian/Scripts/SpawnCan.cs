using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCan : MonoBehaviour
{
    public GameObject Can;
    public Transform spawnPoint1;
    public bool hasCan = true; //this bool tells us whether or not there is a can in your inventory
    private KeyCode item = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(item))
        {
            if (hasCan)
            {
                Instantiate(Can, spawnPoint1.position, spawnPoint1.rotation);
            }
            
        }
    }
}