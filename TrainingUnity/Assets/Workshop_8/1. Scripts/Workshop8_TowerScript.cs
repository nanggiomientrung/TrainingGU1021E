using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop8_TowerScript : MonoBehaviour
{
    [SerializeField] private Workshop8_GameManager gameManager;
    public float towerRadius = 1.28f;
    [SerializeField] private float seekTargetCooldown = 0.1f;
    [SerializeField] private float attackTargetCooldown = 1f;
    [SerializeField] private float detectSqrRadius = 9; // = 3^2
    private float seekTargetTimer;
    private float attackTargetTimer;
    private void Awake()
    {
        gameManager.SetTower(this);
    }

    void Update()
    {
        seekTargetTimer += Time.deltaTime;
        if (seekTargetTimer > seekTargetCooldown && (target == null || target.gameObject.activeSelf == false))
        {
            seekTargetTimer = 0;
            SeekTarget();
        }

        attackTargetTimer += Time.deltaTime;
        if (attackTargetTimer > attackTargetCooldown && target != null && target.gameObject.activeSelf == true)
        {
            attackTargetTimer = 0;
            ShootOnTarget();
        }
    }

    #region ATTACK TARGET
    [SerializeField] private Workshop8_Bullet bulletPrefab;
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private Transform firePos;
    [SerializeField] private float towerDamage = 30;
    private Workshop8_Bullet currentBullet;

    private void ShootOnTarget()
    {
        currentBullet = SimplePool.Spawn(bulletPrefab);
        currentBullet.transform.parent = bulletContainer;
        currentBullet.transform.localPosition = firePos.position;
        currentBullet.SetInfo((target.transform.position - firePos.position).normalized, target, target.radius, towerDamage);
    }
    #endregion ATTACK TARGET

    #region AUTO TARGET
    private Workshop8_Enemy target;
    private void SeekTarget()
    {
        float sqrDistance = 10000; // = 100^2
        float minDistance = 10000;
        int minDistanceIndex = -1; // lưu index của mob gần nhất
        foreach (var enemy in gameManager.enemiesDict.Values)
        {
            sqrDistance = GetSqrDistance(transform, enemy.transform);
            if (minDistance > sqrDistance && sqrDistance <= detectSqrRadius)
            {
                minDistance = sqrDistance;
                minDistanceIndex = enemy.enemyIndex;
            }
        }
        if (minDistanceIndex > -1)
        {
            target = gameManager.enemiesDict[minDistanceIndex];
        }
    }

    private float GetSqrDistance(Transform transformA, Transform transformB)
    {
        return (transformA.position - transformB.position).sqrMagnitude;
    }

    private Vector3 GetDirection(Transform targetTransform)
    {
        return (targetTransform.position - transform.position).normalized;
    }
    #endregion AUTO TARGET

}