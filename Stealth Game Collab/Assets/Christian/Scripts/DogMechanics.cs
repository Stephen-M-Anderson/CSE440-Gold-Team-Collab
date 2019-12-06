using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMechanics : MonoBehaviour
{

    public float speed;
    public float speedModifier = 0f;
    private float minumumSpeed = 1f;
    public GameObject player;
    public ClosestWaypoint closestWaypointCopy;
    public PlayerWalking pwCopy;
    public List<Transform> listOfTransforms;

    // Start is called before the first frame update
    void Start()
    {
        listOfTransforms = new List<Transform>();
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int countTemp = listOfTransforms.Count;
        if (listOfTransforms.Count == 0)
        {
            listOfTransforms.Add(closestWaypointCopy.transform);
        }
        if (listOfTransforms[countTemp - 1] != closestWaypointCopy.transform)
        {
            listOfTransforms.Add(closestWaypointCopy.transform);
        }
        
        FollowNodePath();
        speed = (speedModifier/listOfTransforms.Count) + minumumSpeed;

        Debug.Log(listOfTransforms.Count);
    }

    public void FollowNodePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, listOfTransforms[0].position, speed * Time.deltaTime); // head to the next node in the array
        if (Vector2.Distance(transform.position, listOfTransforms[0].position) < 0.1f) // if we're at the node, start going to the next one
        {
            listOfTransforms.RemoveAt(0);
        }
        else
        {
            Vector2 direction = new Vector2(listOfTransforms[0].position.x - transform.position.x, listOfTransforms[0].position.y - transform.position.y);
            direction = direction.normalized;
            transform.up = direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Insert code of adding the soda can to your inventory here!

            pwCopy.maxSpeed = 0f;
        }
    }
}
