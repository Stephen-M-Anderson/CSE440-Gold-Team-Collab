using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public bool gameOverState = false;
    public bool friendly;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (friendly)
            {
                Debug.Log("This enemy won't hurt you because you smell really good!");
            } else
            {
                //This bool determines whether or not a game over has occurred.
                gameOverState = true;
            }
        }


    }

}
