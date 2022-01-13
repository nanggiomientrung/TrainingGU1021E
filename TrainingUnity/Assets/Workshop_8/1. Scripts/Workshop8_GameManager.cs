using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop8_GameManager : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(AutoSpawnEnemy());
    }

    public void OnEnemyDie(int DieEnemyIndex)
    {
        SimplePool.Despawn(enemiesDict[DieEnemyIndex].gameObject);
        enemiesDict.Remove(DieEnemyIndex);
    }

    public Workshop8_TowerScript tower { get; private set; }

    public void SetTower(Workshop8_TowerScript Tower)
    {
        tower = Tower;
    }

    #region SPAWN ENEMY
    [SerializeField] private Workshop8_Enemy enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    public Dictionary<int, Workshop8_Enemy> enemiesDict = new Dictionary<int, Workshop8_Enemy>();
    private int currentEnemyIndex = -1;

    private Workshop8_Enemy currentEnemy;

    IEnumerator AutoSpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        currentEnemy = SimplePool.Spawn(enemyPrefab);
        currentEnemy.transform.parent = enemyContainer;
        currentEnemyIndex++;
        currentEnemy.ResetState(this, currentEnemyIndex);
        enemiesDict.Add(currentEnemyIndex, currentEnemy);

        currentEnemy.transform.position = new Vector3(Random.Range(0, 2) == 0 ? Random.Range(-7f, -3.5f) : Random.Range(3.5f, 7f), Random.Range(-4f, 4f));
    }
    #endregion SPAWN ENEMY
}
