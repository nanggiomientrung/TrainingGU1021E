using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookey_Enemy : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float minX, maxX;
    private bool isMovingRight = true;
    void Update()
    {
        if (isMovingRight)
        {
            transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);

            if (transform.position.x >= maxX)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);

            if (transform.position.x <= minX)
            {
                isMovingRight = true;
            }
        }
    }
}
