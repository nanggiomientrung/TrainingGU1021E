using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Workshop_3 : MonoBehaviour, TakeDamageInterface
{
    [SerializeField] private HealthBarScript healthBar;
    void Start()
    {
        healthBar.InitHealthBar(100);
    }

    public void TakeDamage(float takenDamage)
    {
        healthBar.ChangeLife(-takenDamage);
    }

    public ActorType GetActorType()
    {
        return ActorType.Enemy;
    }
}