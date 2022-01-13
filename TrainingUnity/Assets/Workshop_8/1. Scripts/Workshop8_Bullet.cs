using UnityEngine;

public class Workshop8_Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float bulletRadius;

    private Vector3 direction; // vector có giá trị độ dài = 1
    private Workshop8_Enemy target;
    private float sqrCollisionRadius;
    private float damage;
    public void SetInfo(Vector3 Direction, Workshop8_Enemy Target, float TargetRadius, float Damage)
    {
        damage = Damage;
        direction = Direction;
        target = Target;
        sqrCollisionRadius = Mathf.Pow(bulletRadius + TargetRadius, 2);

        transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(Vector2.right, Direction);
    }

    private void Update()
    {
        transform.position += velocity * direction * Time.deltaTime;

        if (target.gameObject.activeSelf == false) return;

        if ((transform.position - target.transform.position).sqrMagnitude < sqrCollisionRadius)
        {
            target.TakeDamage(damage);
            SimplePool.Despawn(gameObject);
        }

        if (transform.position.y < -5 || transform.position.y > 5 || transform.position.x < -10 || transform.position.x > 10)
        {
            SimplePool.Despawn(gameObject);
        }
    }
}