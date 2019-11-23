using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanThrowingScript : MonoBehaviour
{
    public KeyCode item = KeyCode.Space;
    public float canSpeed;
    public float canDistance;
    public GameObject SodaCan;
    private float canDistanceOriginal;
    private bool isThrowing;
    private Vector2 canStart;
    public Rigidbody2D rb;
    public Rigidbody2D rbPlayer;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - rbPlayer.position.x, mousePosition.y - rbPlayer.position.y);
        direction = direction.normalized;

        if (Input.GetKeyDown(item) && !isThrowing)     
        {
            Instantiate(SodaCan, spawnPoint.position, spawnPoint.rotation);
            rb.AddForce(direction * canSpeed, ForceMode2D.Impulse); //this is the rb of the can
            isThrowing = true;
            canStart = rbPlayer.position;
            canDistanceOriginal = canDistance;
            if (Vector2.Distance(mousePosition, canStart) < canDistance)
            {
                canDistance = Vector2.Distance(mousePosition, canStart) - 0.2f;
            }
        }
        if (isThrowing) 
        {
            if (Vector2.Distance(rbPlayer.position, canStart) >= canDistance) 
            {
                isThrowing = false;
                rbPlayer.velocity = Vector2.zero;
                if (canDistance != canDistanceOriginal)
                {
                    canDistance = canDistanceOriginal;
                }
            }
        }
    }
}
