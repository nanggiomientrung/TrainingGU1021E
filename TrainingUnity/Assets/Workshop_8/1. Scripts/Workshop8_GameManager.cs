using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop8_GameManager : MonoBehaviour
{
    private void Start()
    {
        SpawnEnemy();
    }

    public void OnEnemyDie(int DieEnemyIndex)
    {
        SpawnEnemy();
        SimplePool.Despawn(enemiesDict[DieEnemyIndex].gameObject);
        enemiesDict.Remove(DieEnemyIndex);
    }
    #region SPAWN ENEMY
    [SerializeField] private Workshop8_Enemy enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    public Dictionary<int, Workshop8_Enemy> enemiesDict = new Dictionary<int, Workshop8_Enemy>();
    private int currentEnemyIndex = -1;

    private Workshop8_Enemy currentEnemy;
    private void SpawnEnemy()
    {
        currentEnemy = SimplePool.Spawn(enemyPrefab);
        currentEnemy.transform.parent = enemyContainer;
        currentEnemyIndex++;
        currentEnemy.ResetState(this, currentEnemyIndex);
        enemiesDict.Add(currentEnemyIndex, currentEnemy);

        currentEnemy.transform.position = new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
    }
    #endregion SPAWN ENEMY
}
