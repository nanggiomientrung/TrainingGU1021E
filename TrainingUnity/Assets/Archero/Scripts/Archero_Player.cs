using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archero_Player : MonoBehaviour
{
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float velocityX;

    [SerializeField] private Archero_Bullet bulletPrefab;
    [SerializeField] private Transform firePos;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }

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

    private void Shot()
    {
        Archero_Bullet tempBullet = Instantiate(bulletPrefab);
        tempBullet.transform.position = firePos.position + new Vector3(0, 0, -0.1f);

        tempBullet.SetVelocity(Vector2.up * 10, Random.Range(6, 10));
    }
}
