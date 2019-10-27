using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode interact = KeyCode.E;
    public KeyCode dash = KeyCode.Space;
    public KeyCode cover = KeyCode.LeftControl;

    [Header("Movement settings")]
    public float moveSpeed;
    public float dashSpeed;
    public float dashDistance;
    private float dashDistanceOriginal;
    private bool isDashing;
    private Vector2 dashStart;

    public Rigidbody2D rb;
    private CoverScript cc;

    // Start is called before the first frame 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CoverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Movement
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        direction = direction.normalized;
        if(!cc.inCover)
            transform.up = direction;
        mousePosition.z = -10;

        Vector2 newMovement;
        if (!cc.inCover)
        {
            if (Input.GetKey(moveRight) & !isDashing)
            {
                newMovement = new Vector2(rb.position.x + (moveSpeed * Time.deltaTime), rb.position.y);
                rb.position = newMovement;
            }
            if (Input.GetKey(moveLeft) & !isDashing)
            {
                newMovement = new Vector2(rb.position.x - (moveSpeed * Time.deltaTime), rb.position.y);
                rb.position = newMovement;
            }
            if (Input.GetKey(moveUp) & !isDashing)
            {
                newMovement = new Vector2(rb.position.x, rb.position.y + (moveSpeed * Time.deltaTime));
                rb.position = newMovement;
            }
            if (Input.GetKey(moveDown) & !isDashing)
            {
                newMovement = new Vector2(rb.position.x, rb.position.y - (moveSpeed * Time.deltaTime));
                rb.position = newMovement;
            }
        }
        else if (cc.inCover) // if the player is in cover
        {
            if (cc.coverSide == 1 || cc.coverSide == 3) // player is against horizontal cover (they are facing up or down)
            {
                if (cc.atCoverCorner) // if the player is at a corner of their cover
                {
                    if (cc.whichCornerBool == true) // if the player is at the top or bottom right corner, player can only move left
                    {
                        if (Input.GetKey(moveLeft))
                        {
                            newMovement = new Vector2(rb.position.x - (moveSpeed * Time.deltaTime), rb.position.y);
                            rb.position = newMovement;
                        }
                    }
                    else                            // if the player is at the top or bottom left corner, player can only move right
                    {
                        if (Input.GetKey(moveRight))
                        {
                            newMovement = new Vector2(rb.position.x + (moveSpeed * Time.deltaTime), rb.position.y);
                            rb.position = newMovement;
                        }
                    } 
                }
                else                                // the player is not at a corner of their cover, they can move left or right
                {
                    if (Input.GetKey(moveRight))
                    {
                        newMovement = new Vector2(rb.position.x + (moveSpeed * Time.deltaTime), rb.position.y);
                        rb.position = newMovement;
                    }
                    if (Input.GetKey(moveLeft))
                    {
                        newMovement = new Vector2(rb.position.x - (moveSpeed * Time.deltaTime), rb.position.y);
                        rb.position = newMovement;
                    }
                }
            }
            else if (cc.coverSide == 2 || cc.coverSide == 4) // player is against verticle cover
            {
                if (cc.atCoverCorner) // if the player is at a corner of their cover
                {
                    if (cc.whichCornerBool == true) // if the player is at the top left or right corner, player can only move down
                    {
                        if (Input.GetKey(moveDown))
                        {
                            newMovement = new Vector2(rb.position.x, rb.position.y - (moveSpeed * Time.deltaTime));
                            rb.position = newMovement;
                        }
                    }
                    else                            // if the player is at the bottom left or right corner, player can only move up
                    {
                        if (Input.GetKey(moveUp))
                        {
                            newMovement = new Vector2(rb.position.x, rb.position.y + (moveSpeed * Time.deltaTime));
                            rb.position = newMovement;
                        }
                    }
                }
                else                                // the player is not at a corner of their cover, they can move up or down
                {
                    if (Input.GetKey(moveUp))
                    {
                        newMovement = new Vector2(rb.position.x, rb.position.y + (moveSpeed * Time.deltaTime));
                        rb.position = newMovement;
                    }
                    if (Input.GetKey(moveDown))
                    {
                        newMovement = new Vector2(rb.position.x, rb.position.y - (moveSpeed * Time.deltaTime));
                        rb.position = newMovement;
                    }
                }
            }
        }
        /*        if (Input.GetKeyDown(dash) && rb.velocity.sqrMagnitude != 0)
                {
                    rb.AddForce(rb.velocity * dashSpeed, ForceMode2D.Impulse);
                    isDashing = true;
                    dashStart = rb.position;
                    dashDistanceOriginal = dashDistance;
                }
                if (isDashing) // stops dashing once the player has reached the dash distance
                {
                    if (Vector2.Distance(rb.position, dashStart) >= dashDistance) 
                    {
                        isDashing = false;
                        rb.velocity = Vector2.zero;
                    }
                }
        */
        // WORKING CODE for dashing the player towards the mouse cursor and ending the dash early if the mouse is closer than the given dash distance.
        // Could possibly be repurposed for throwing distraction items.
        /* if (Input.GetKeyDown(dash) && !isDashing)     
        {
            rb.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
            isDashing = true;
            dashStart = rb.position;
            dashDistanceOriginal = dashDistance;
            if (Vector2.Distance(mousePosition, dashStart) < dashDistance)
            {
                dashDistance = Vector2.Distance(mousePosition, dashStart) - 0.2f;
            }
        }
        if (isDashing) // stops dashing once the player has reached the dash distance
        {
            if (Vector2.Distance(rb.position, dashStart) >= dashDistance) 
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
                if (dashDistance != dashDistanceOriginal)
                {
                    dashDistance = dashDistanceOriginal;
                }
            }
        }*/
    }
}
