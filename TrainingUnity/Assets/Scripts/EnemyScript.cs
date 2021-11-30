using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float velocityX;

    private bool isMovingRight = true;
    
    void Update()
    {
        if(isMovingRight)
        {
            transform.position += new Vector3(velocityX * Time.deltaTime, 0, 0);

            if(transform.position.x >= rightX)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position -= new Vector3(velocityX * Time.deltaTime, 0, 0);

            if (transform.position.x <= leftX)
            {
                isMovingRight = true;
            }
        }
    }
}
