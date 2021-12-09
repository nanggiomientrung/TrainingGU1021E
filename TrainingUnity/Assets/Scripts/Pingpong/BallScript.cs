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
                PingPong_GameManager.instance.ChangeScore(true);
            }
            else
            {
                // enemy get score
                PingPong_GameManager.instance.ChangeScore(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError("trigger enter" + collision.gameObject.name);
    }
}
