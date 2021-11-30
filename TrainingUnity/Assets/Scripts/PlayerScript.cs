using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float velocityX;
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left
            transform.position -= new Vector3(velocityX * Time.deltaTime, 0, 0);

            if (transform.position.x <= leftX)
            {
                transform.position = new Vector3(leftX, transform.position.y, transform.position.z);
            }
            return;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // move right
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + velocityX * Time.deltaTime, leftX, rightX), transform.position.y, transform.position.z);

            // Math.Clamp(value, min, max) => value neu' min < value < max, => min neu' value < min, => max neu' value > max
            return;
        }
    }
}
