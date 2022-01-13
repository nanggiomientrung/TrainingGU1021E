using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop8_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
