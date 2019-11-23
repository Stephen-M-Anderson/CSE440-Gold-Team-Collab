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
    public bool hasCan;
    private Vector2 canStart;
    public Rigidbody2D rbSodaCan;
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
        //Vector2 direction = new Vector2(mousePosition.x, mousePosition.y);
        direction = direction.normalized;

        if (Input.GetKeyDown(item) && !isThrowing && hasCan)     
        {
            Instantiate(SodaCan, spawnPoint.position, spawnPoint.rotation);
            //rbSodaCan = GetComponent<Rigidbody2D>(); //Pretty sure these two lines aren't needed I just added them to see if it solved my problem and it didn't
            //rbPlayer = GetComponent<Rigidbody2D>();
            rbSodaCan.AddForce(direction * canSpeed, ForceMode2D.Impulse); //this is the rb of the can
            isThrowing = true;
            canStart = spawnPoint.position;
            canDistanceOriginal = canDistance;
            if (Vector2.Distance(mousePosition, canStart) < canDistance)
            {
                canDistance = Vector2.Distance(mousePosition, canStart) - 0.2f;
            }
        }
        if (isThrowing) 
        {
            if (Vector2.Distance(mousePosition, canStart) >= canDistance) 
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
