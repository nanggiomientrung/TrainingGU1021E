using UnityEngine;

public class TRex_Obstacle : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float leftX;
    void Update()
    {
        transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
        if(transform.position.x <= leftX)
        {
            Destroy(gameObject);
        }
    }
}
