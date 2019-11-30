using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCanMachine : MonoBehaviour
{
    public GameObject Can;
    public Transform spawnPoint1;
    public bool hasCan = true; //this bool tells us whether or not there is a can in your inventory
    private KeyCode interactMachine = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKeyDown(interactMachine))
        {

            if (hasCan)
            {
                Instantiate(Can, spawnPoint1.position, spawnPoint1.rotation);
                hasCan = false;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasCan = true;
    }
}
