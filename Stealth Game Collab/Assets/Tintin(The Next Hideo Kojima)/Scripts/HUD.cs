using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public CoverScript coverscript;
    public string walking = "walking";
    public string standing = "standing";
    public Text stance;
    bool isMoving = false;

    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode interact = KeyCode.E;
    public KeyCode dash = KeyCode.Space;
    public KeyCode cover = KeyCode.LeftControl;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveLeft) || Input.GetKey(moveRight) || Input.GetKey(moveUp) || Input.GetKey(moveDown))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving)
        {
            stance.text = walking;
            if(coverscript.inCover)
            {
                stance.text = "taking cover";
            }
        }
       
        else
        {
            stance.text = standing;
            if (coverscript.inCover)
            {
                stance.text = "taking cover";
            }
        }
    }
}
