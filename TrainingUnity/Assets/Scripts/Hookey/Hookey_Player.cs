using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookey_Player : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float minX, minY, maxX, maxY;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += velocity * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += velocity * Time.deltaTime * Vector3.right;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += velocity * Time.deltaTime * Vector3.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += velocity * Time.deltaTime * Vector3.down;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0);
    }
}
