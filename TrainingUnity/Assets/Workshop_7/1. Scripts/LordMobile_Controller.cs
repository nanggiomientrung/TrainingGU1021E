using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordMobile_Controller : MonoBehaviour
{
    public Dictionary<int, LordMobileMob> enemiesDict = new Dictionary<int, LordMobileMob>();
    public Dictionary<int, LordMobileMob> alliesDict = new Dictionary<int, LordMobileMob>();

    [SerializeField] private LordMobileMob mobPrefab;
    LordMobileMob tempMob;
    private void Start()
    {
        StartCoroutine(AutoSpawnMobs());
    }
    IEnumerator AutoSpawnMobs()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            SpawnEnemies();
            SpawnAllies();
        }
    }


    int currentMobIndex = 0;
    private void SpawnEnemies()
    {
        tempMob = SimplePool.Spawn(mobPrefab);
        currentMobIndex++;
        tempMob.SetData(currentMobIndex, true, this);
        tempMob.transform.position = new Vector3(Random.Range(-6.3f, -2.6f), Random.Range(-4.1f, 4.1f), 0);
        
        enemiesDict.Add(currentMobIndex, tempMob);

        tempMob.name = "Mob_Enemy";
    }
    private void SpawnAllies()
    {
        tempMob = SimplePool.Spawn(mobPrefab);
        currentMobIndex++;
        tempMob.SetData(currentMobIndex, false, this);
        tempMob.transform.position = new Vector3(Random.Range(2.6f, 6.3f), Random.Range(-4.1f, 4.1f), 0);
        alliesDict.Add(currentMobIndex, tempMob);

        tempMob.name = "Mob_Ally";
    }
}