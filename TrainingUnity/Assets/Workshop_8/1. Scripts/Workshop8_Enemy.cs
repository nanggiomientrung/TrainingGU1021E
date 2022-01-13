using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop8_Enemy : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Vector3 direction; // vector có giá trị độ dài = 1
    void Update()
    {
        transform.position += velocity * direction * Time.deltaTime;
        if (gameManager != null && gameManager.tower != null && gameManager.tower.gameObject.activeSelf == true)
        {
            direction = (gameManager.tower.transform.position - transform.position).normalized;
        }
    }

    Workshop8_GameManager gameManager;
    public int enemyIndex { get; private set; }
    [SerializeField] private float baseLife = 100;
    public float radius = 0.5f;
    public void ResetState(Workshop8_GameManager GameManager, int EnemyIndex)
    {
        gameManager = GameManager;
        currentLife = baseLife;
        enemyIndex = EnemyIndex;
        direction = Vector3.zero;
    }

    #region TAKE DMG
    private float currentLife;
    public void TakeDamage(float takenDamage)
    {
        currentLife -= takenDamage;
        if (currentLife <= 0)
        {
            gameManager.OnEnemyDie(enemyIndex);
        }
    }
    #endregion TAKE DMG
}
