using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            // win game
            if(transform.position.x > 0)
            {
                // player get score
                GameManager.instance.ChangeScore(true);
            }
            else
            {
                // enemy get score
                GameManager.instance.ChangeScore(false);
            }
        }
    }
}
