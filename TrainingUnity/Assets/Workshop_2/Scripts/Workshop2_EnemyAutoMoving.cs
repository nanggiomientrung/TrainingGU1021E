using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop2_EnemyAutoMoving : MonoBehaviour
{
    private void Start()
    {
        Workshop2_GameManager.instance.OnPauseGame += PauseGame;
        Workshop2_GameManager.instance.OnResumeGame += ResumeGame;
    }

    private bool isMoving = true;
    private void PauseGame(string eventString)
    {
        Debug.LogError(eventString);
        isMoving = false;
    }

    private void ResumeGame()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(-5 * Time.deltaTime, 0, 0);
        }

        if (transform.position.x < -9)
        {
            SimplePool.Despawn(gameObject);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    private float selfDamage;

    private ActorType targetActorType = ActorType.Enemy;
    public void SetDamageForFrog(float damage)
    {
        selfDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError($"Hit damage: {selfDamage}");
        TakeDamageInterface takeDamageInterface = collision.GetComponent<TakeDamageInterface>();

        if (takeDamageInterface.GetActorType() == targetActorType)
        {
            takeDamageInterface.TakeDamage(selfDamage);
            SimplePool.Despawn(gameObject);
        }
        else
        {
            //....
        }
    }
}
