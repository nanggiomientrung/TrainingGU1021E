using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookey_Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D selfRigidBody2D;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "GoalTop")
        {
            // player get score
            Hookey_GameManager.instance.ChangeScore(true);
        }
        if (collision.transform.name == "GoalBot")
        {
            // enemy get score
            Hookey_GameManager.instance.ChangeScore(false);
        }

        if (collision.transform.name == "Player" || collision.transform.name == "Enemy")
        {
            AddForceWhenCollision((Vector2)transform.position - (Vector2)collision.transform.position);
        }
    }

    private void AddForceWhenCollision(Vector2 ForceDirection)
    {
        selfRigidBody2D.AddForce(ForceDirection * 2, ForceMode2D.Impulse);

        selfRigidBody2D.velocity = selfRigidBody2D.velocity.normalized * 8;
    }
}
