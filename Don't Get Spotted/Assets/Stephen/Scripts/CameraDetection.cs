using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    private Collider2D colcopy;
    private SecurityCamera sccopy;
    [SerializeField]
    private LayerMask visionBlockers;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private float detectionRadius;
    private bool isOverlapped = false;
    // Start is called before the first frame update
    void Start()
    {
        sccopy = GetComponentInParent<SecurityCamera>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isOverlapped = Physics2D.OverlapCircle(sccopy.transform.position, detectionRadius, playerLayer);
        if (col.gameObject.tag == "Player" && isOverlapped == false)
        {
            Vector2 direction = new Vector2(col.transform.position.x - transform.position.x, col.transform.position.y - transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, col.transform.position), visionBlockers);
            if (hit.collider == null) // there is nothing blocking the camera from seeing the player
            {
                sccopy.trackingPlayer = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        isOverlapped = Physics2D.OverlapCircle(sccopy.transform.position, detectionRadius, playerLayer);
        if (col.gameObject.tag == "Player" && isOverlapped == false)
        {
            Vector2 direction = new Vector2(col.transform.position.x - transform.position.x, col.transform.position.y - transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,  Vector2.Distance(transform.position, col.transform.position), visionBlockers);
            if (hit.collider != null && sccopy.trackingPlayer == true)
            {
                sccopy.trackingPlayer = false;
            }
            else if (hit.collider == null)
            {
                sccopy.trackingPlayer = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            sccopy.trackingPlayer = false;
        }
    }
}
