using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour
{
    public float coverRange; // How far away the player can be from the wall to enter cover
    private PlayerWalking playerStats; // allows this script to access the PlayerWalking script
    public bool inCover; // if true, the player is in cover. 
    public LayerMask wall; // which Layer will be considered cover by the script
    public Vector2 coverPoint; // stores the Vector2 position for the closest point on the perimter of a cover's collider, ie where the player will move to when in cover
    private Vector2 direction;
    public int coverSide; // 0 = not aligned on axis, 1 = top / north, 2 = right / east, 3 = bottom / south, 4 = left / west
    public bool atCoverCorner; // if true, the player is at one of the corners 
    public float distanceToCorner;
    public float cornerDistanceThreshold;
    public float speedMultiplier;
    public bool whichCornerBool; // if true / 1, player is at the corner with highest position magnitude (ie right or top), else player is at the other corner

    public Collider2D nearestCover; // stores the Collder2D compontnent for the nearest valid cover to the player
    private ContactFilter2D filter;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerWalking>(); // instantiate PlayerStats as the PlayerWalking script
        inCover = false;
        // filter = filter.NoFilter(); // sets the Contact Filter to No Filter
        // coverList = new Collider2D[4];
    }

    // FixedUpdate is called once per fixed frame
    // Use Fixed Update for anything involving Rigid Bodies
    void FixedUpdate()
    {
        nearestCover = Physics2D.OverlapCircle(playerStats.rb.position, coverRange, wall);
        if (Input.GetKey(playerStats.cover))
        { 
            if (nearestCover) // if the player is within range of valid cover based on coverRange and not in cover
            {
                coverPoint = Physics2D.ClosestPoint(playerStats.rb.position, nearestCover); // finds the closest point on the perimeter of the collider and stores it in coverPoint
                direction = new Vector2(playerStats.rb.position.x - coverPoint.x, playerStats.rb.position.y - coverPoint.y); // makes direction a vector pointing perp. away from the cover
                playerStats.rb.position = Physics2D.ClosestPoint(playerStats.rb.position, nearestCover); // moves the player to the edge of the cover
                playerStats.transform.up = direction; // makes the player face away from the cover

                if (!inCover) // if player is just entering cover
                {
                    playerStats.moveSpeed = playerStats.moveSpeed * speedMultiplier;
                    inCover = true;
                    Debug.Log("Extents: " + nearestCover.bounds.extents);

                    if (playerStats.transform.up == Vector3.up)
                    {
                        coverSide = 1;
                    }
                    else if (playerStats.transform.up == Vector3.right)
                    {
                        coverSide = 2;
                    }
                    else if (playerStats.transform.up == Vector3.down)
                    {
                        coverSide = 3;
                    }
                    else if (playerStats.transform.up == Vector3.left)
                    {
                        coverSide = 4;
                    }
                    else
                        coverSide = 0;
                }
                
                if (coverSide == 1 || coverSide == 3) // if player is taking cover on the top or bottom of the cover 
                {
                    if (playerStats.rb.position.x < nearestCover.bounds.center.x) // the player is on the left half of the cover
                    {
                        distanceToCorner = playerStats.rb.position.x - nearestCover.bounds.min.x;
                        if (distanceToCorner < cornerDistanceThreshold) // player is at left corner
                        {
                            atCoverCorner = true;
                            whichCornerBool = false;
                        }
                        else
                            atCoverCorner = false;
                    }
                    else if (playerStats.rb.position.x > nearestCover.bounds.center.x) // player is on the right half of the cover
                    {
                        distanceToCorner = playerStats.rb.position.x - nearestCover.bounds.max.x;
                        if (distanceToCorner > 0 - cornerDistanceThreshold) // player is at right corner
                        {
                            atCoverCorner = true;
                            whichCornerBool = true;
                        }
                        else
                            atCoverCorner = false;
                    }
                }
                else if (coverSide == 2 || coverSide == 4) // if the player is taking cover on the left or right of the cover
                {
                    if (playerStats.rb.position.y < nearestCover.bounds.center.y) // player is in the bottom half of the cover
                    {
                        distanceToCorner = playerStats.rb.position.y - nearestCover.bounds.min.y;
                        if (distanceToCorner < cornerDistanceThreshold) // player is at bottom corner
                        {
                            atCoverCorner = true;
                            whichCornerBool = false;
                        }
                        else
                            atCoverCorner = false;
                    }
                    else if (playerStats.rb.position.y > nearestCover.bounds.center.y) // player is in the top half of the cover
                    {
                        distanceToCorner = playerStats.rb.position.y - nearestCover.bounds.max.y;
                        if (distanceToCorner > 0 - cornerDistanceThreshold) // player is at top corner
                        {
                            atCoverCorner = true;
                            whichCornerBool = true;
                        }
                        else
                            atCoverCorner = false;
                    }
                }
            }
        }
        if (!Input.GetKey(playerStats.cover) && inCover == true) // if cover button isn't pressed, inCover = false
        { 
            inCover = false;
            atCoverCorner = false;
            playerStats.peeking = false;
            playerStats.moveSpeed = playerStats.moveSpeed / speedMultiplier;
            playerStats.rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}

// The below commented out code is a WIP for selecting the correct cover object when multiple are within range
/*
coverCount = Physics2D.OverlapCircle(playerStats.rb.position, coverRange, filter, coverList); // Finds all cover within coverRange, stores the covers in coverList
Debug.Log("Cover count = " + coverCount);
for (int i = 0; i < coverCount; ++i)
{
    if (i == 0)
    {
        nearestCover = coverList[0];
        distanceToCover = Vector2.Distance(playerStats.rb.position, nearestCover.ClosestPoint(playerStats.rb.position));
    }
    else
    {
        tempDTC = Vector2.Distance(playerStats.rb.position, coverList[i].ClosestPoint(playerStats.rb.position));
        if (tempDTC < distanceToCover)
        {
            nearestCover = coverList[i];
            distanceToCover = tempDTC;
        }
    }
}
Debug.Log("Distance to Cover = " + distanceToCover);
if (Input.GetKey(playerStats.cover)) // if the player is pressing or holding the Cover input in PlayerWalking
{

} */
