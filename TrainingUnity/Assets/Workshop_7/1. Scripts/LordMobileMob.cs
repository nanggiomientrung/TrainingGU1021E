using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordMobileMob : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int mobIndex { get; private set; }
    bool isEnemy;
    LordMobile_Controller controller;
    LordMobileMob target;
    // Set enemy / ally
    public void SetData(int MobIndex, bool IsEnemy, LordMobile_Controller Controller)
    {
        if (IsEnemy)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
        mobIndex = MobIndex;
        isEnemy = IsEnemy;
        controller = Controller;
        currentHP = healthPoint;
    }

    private void Update()
    {
        if (target != null && target.gameObject.activeSelf == true)
        {
            if (GetSqrDistance(transform, target.transform) > 1)
            {
                AimTarget();
            }
            else
            {
                AttackTarget();
            }
            return;
        }

        SeekTarget();
    }

    private void SeekTarget()
    {
        float sqrDistance = 10000; // = 100^2
        float minDistance = 10000;
        int minDistanceIndex = -1; // lưu index của mob gần nhất
        if (isEnemy)
        {
            foreach (var mob in controller.alliesDict.Values)
            {
                sqrDistance = GetSqrDistance(transform, mob.transform);
                if (minDistance > sqrDistance)
                {
                    minDistance = sqrDistance;
                    minDistanceIndex = mob.mobIndex;
                }
            }
            if (minDistanceIndex > -1)
            {
                target = controller.alliesDict[minDistanceIndex];
            }
        }
        else
        {
            foreach (var mob in controller.enemiesDict.Values)
            {
                sqrDistance = GetSqrDistance(transform, mob.transform);
                if (minDistance > sqrDistance)
                {
                    minDistance = sqrDistance;
                    minDistanceIndex = mob.mobIndex;
                }
            }
            if (minDistanceIndex > -1)
            {
                target = controller.enemiesDict[minDistanceIndex];
            }
        }
    }

    [Header("Move Stats")]
    [Tooltip("Vận tốc di chuyển của mob khi đuổi theo đối phương")]
    [SerializeField] private float velocity;
    private void AimTarget()
    {
        transform.position += GetDirection(target.transform) * velocity * Time.deltaTime;
    }

    private float attackTimer;
    [SerializeField] private float attackCooldown = 1;
    private void AttackTarget()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            attackTimer -= attackCooldown;
            target.TakeDamage();
        }
    }

    // trả về square distance giữa 2 object
    private float GetSqrDistance(Transform transformA, Transform transformB)
    {
        return (transformA.position - transformB.position).sqrMagnitude;
    }

    private Vector3 GetDirection(Transform targetTransform)
    {
        return (targetTransform.position - transform.position).normalized;
    }

    [Header("Mob Stats")]
    [SerializeField] private int healthPoint = 100;
    [SerializeField] private int damage = 25;
    private int currentHP;
    public void TakeDamage()
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            SimplePool.Despawn(gameObject);
            if (isEnemy)
            {
                controller.enemiesDict.Remove(mobIndex);
            }
            else
            {
                controller.alliesDict.Remove(mobIndex);
            }
        }
    }
}
