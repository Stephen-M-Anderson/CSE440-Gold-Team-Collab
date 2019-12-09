using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask visionBlockers;
    private GuardMechanics gmcopy;
    private GameObject player;
    private ClosestWaypoint playercw;
    private GameObject tempNode;

    private void Start()
    {
        gmcopy = GetComponentInParent<GuardMechanics>();
        player = GameObject.FindGameObjectWithTag("Player");
        playercw = player.gameObject.GetComponent<ClosestWaypoint>();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            Vector2 direction = new Vector2(o.transform.position.x - transform.position.x, o.transform.position.y - transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, o.transform.position), visionBlockers);
            if (hit.collider == null)
            {
                gmcopy.inView = true;
                Debug.Log("In view set to true in Trigger enter");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D o)
    {
        Vector2 direction = new Vector2(o.transform.position.x - transform.position.x, o.transform.position.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, o.transform.position), visionBlockers);
        if (hit.collider == null) // as long as the player is in sight
        {
            gmcopy.inView = true;
            Debug.Log("In view set to true in trigger stay");
        }
        else // if the player loses line of sight 
        {
            if (gmcopy.inView == true) // if this is the first frame that the player has lost line of sight
            {
                if (tempNode == null)
                {
                    tempNode = playercw.closestWaypoint; // grabs the players waypoint the first frame the player is out of vision, guard continues to chase. 
                }
                else if (playercw.closestWaypoint != tempNode) // waits until the player has moved to the next closest waypoint once out of view, lets the guard path around corners simply 
                {
                    gmcopy.inView = false;
                    gmcopy.isSearching = true;
                    Debug.Log("In view set to false in trigger stay, lost sight");
                    gmcopy.SendMessage("StartMoveToWaypoint", playercw.closestWaypoint); // sends the guard to the players current location 
                    Debug.Log("I think he went this way!");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            gmcopy.inView = false;
            Debug.Log("Must Have Been the Wind");
        }
    }
}