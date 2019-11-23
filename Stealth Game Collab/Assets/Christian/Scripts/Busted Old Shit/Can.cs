using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public float canSpeed = 5.0f;
    private Rigidbody2D rb;
    private float canDistanceOriginal;
    private float canDistance = 5f;
    private Vector3 canStart;
    private bool isThrowing;
    public bool freeze;

    private Vector3 moveDirection;
    private Vector3 initialMousePos;
    private Vector3 currentPosition;
    private Vector3 mousePos;

    private KeyCode boolKey = KeyCode.O;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - rb.position.x, mousePosition.y - rb.position.y);
        direction = direction.normalized;

        Vector3 currentPosition = transform.position;
        canStart = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = mousePos - currentPosition;
        moveDirection.z = 0;
        moveDirection.Normalize();

        Vector3 target = moveDirection * canSpeed + currentPosition;
        rb.AddForce(moveDirection * canSpeed, ForceMode2D.Impulse);

        

        //Destroy(gameObject, 5.0f);
        //rb.AddForce(direction * canSpeed, ForceMode2D.Impulse);

        /*canDistanceOriginal = canDistance;

        if (Vector2.Distance(mousePosition, canStart) < canDistance)
        {
            canDistance = Vector2.Distance(mousePosition, canStart) - 0.2f;
        }

        if (isThrowing)
        {
            if (Vector2.Distance(rb.position, canStart) >= canDistance)
            {
                isThrowing = false;
                rb.velocity = Vector2.zero;
                if (canDistance != canDistanceOriginal)
                {
                    canDistance = canDistanceOriginal;
                }
            }
        }*/

        
    }

    // Update is called once per frame
    void Update()
    {
        //rb = GetComponent<Rigidbody2D>();

        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Vector2 direction = new Vector2(mousePosition.x - rb.position.x, mousePosition.y - rb.position.y);
        //direction = direction.normalized;

        Vector3 currentPosition = transform.position;

        if (Input.GetKeyDown(boolKey))
        {
            if (freeze)
            {
                freeze = false;
            } else
            {
                freeze = true;
            }
        }

        if (freeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //if (isThrowing)
        //{
        if (Vector3.Distance(currentPosition, canStart) >= canDistance)
            {
                //isThrowing = false;
                rb.velocity = Vector2.zero;
                if (canDistance != canDistanceOriginal)
                {
                    canDistance = canDistanceOriginal;
                }
        }
        //}

        //if (currentPosition == moveToward)
        // {
        //   rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //}


    }
}
