using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerCopy;
    private PlayerWalking pw; 
    public float zoom;
    public float camSpeed;
    public float peekDistance;
    private float peekSpeed;

    private Vector3 peekTarget; // point where the camera should move when peeking
    // Start is called before the first frame update
    void Start()
    {
        pw = playerCopy.GetComponent<PlayerWalking>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        peekSpeed = camSpeed * Time.deltaTime;
        if (pw.peeking == true)
        {
            if (pw.cc.coverSide == 1)
            {
                if (pw.cc.whichCornerBool == true)  // player is at top right corner facing up, peaks down and right , 12.3, 4.5 target, pos 3.5, 1.8
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x + (peekDistance * .67f), playerCopy.transform.position.y - peekDistance, zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed); 
                }
                else                                // player is at top left corner facing up, peaks down and left
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x - (peekDistance * .67f), playerCopy.transform.position.y - peekDistance, zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
            }
            else if (pw.cc.coverSide == 2)          
            {
                if (pw.cc.whichCornerBool == true)  // player is at top right corner facing right, peaks up and left
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x - peekDistance, playerCopy.transform.position.y + (peekDistance * .67f), zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
                else                                // player is at bottom right corner facing right, peaks down and left
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x - peekDistance, playerCopy.transform.position.y - (peekDistance * .67f), zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
            }
            else if (pw.cc.coverSide == 3)
            {
                if (pw.cc.whichCornerBool == true)  // player is at bottom right corner facing down, peaks up and right
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x + (peekDistance * .67f), playerCopy.transform.position.y + peekDistance, zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
                else                                // player is at bottom left corner facing down, peaks up and left
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x - (peekDistance * .67f), playerCopy.transform.position.y + peekDistance, zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
            }
            else if (pw.cc.coverSide == 4)
            {
                if (pw.cc.whichCornerBool == true)  // player is at top left corner facing left, peaks up and right
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x + peekDistance, playerCopy.transform.position.y + (peekDistance * .67f), zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
                else                                // player is at bottom left corner facing left, peaks down and right
                {
                    peekTarget = new Vector3(playerCopy.transform.position.x + peekDistance, playerCopy.transform.position.y - (peekDistance * .67f), zoom);
                    transform.position = Vector3.Lerp(transform.position, peekTarget, peekSpeed);
                }
            }

        }
        else
            transform.position = new Vector3(playerCopy.transform.position.x, playerCopy.transform.position.y, zoom);
    }
}
