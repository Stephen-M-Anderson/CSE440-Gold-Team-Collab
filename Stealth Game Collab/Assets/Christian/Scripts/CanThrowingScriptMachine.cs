using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanThrowingScriptMachine : MonoBehaviour
{
    private Rigidbody2D rb; //This is the rigidbody component of the can.

    public float canSpeed = 5.0f; //The spped of the can, no shit.
    public float maxThrowDistance = 1f; //The maximum distance we want a can to be able to be thrown. I made this public so it could easily be edited in the inspector.

    private Vector3 canStart; //The location of the can when it spawns

    // Start is called before the first frame update
    void Start()
    {

        Vector3 currentPosition = transform.position;

        canStart = transform.position;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * canSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentPositionUpdate = transform.position;
        float distanceTraveled = Vector3.Distance(currentPositionUpdate, canStart);

        if (distanceTraveled > maxThrowDistance)
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Insert code of adding the soda can to your inventory here!

            Destroy(gameObject);
        }
    }

}
