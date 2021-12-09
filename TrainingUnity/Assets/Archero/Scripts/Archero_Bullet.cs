using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archero_Bullet : MonoBehaviour
{
    private Vector2 velocity;
    private float maxY;


    void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;

        if (transform.position.y >= maxY)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(Vector2 InputVelocity, float MaxY)
    {
        velocity = InputVelocity;
        maxY = MaxY;
    }
}
