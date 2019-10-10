using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject playerCopy;
    public float zoom;
    // Start is called before the first frame update
    void Start()
    {
        playerCopy = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerCopy.transform.position.x, playerCopy.transform.position.y, zoom);
    }
}
