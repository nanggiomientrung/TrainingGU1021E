using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Workshop_3 : MonoBehaviour, TakeDamageInterface
{
    [SerializeField] private HealthBarScript healthBar;
    void Start()
    {
        healthBar.InitHealthBar(300);
    }


    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    healthBar.ChangeLife(-30);
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            healthBar.ChangeLife(45);
            return;
        }
    }

    public void TakeDamage(float takenDamage)
    {
        healthBar.ChangeLife(-takenDamage);
    }

    public ActorType GetActorType()
    {
        return ActorType.Player;
    }
}

public interface TakeDamageInterface // iTakeDamage
{
    void TakeDamage(float takenDamage);

    ActorType GetActorType();
}

public enum ActorType
{
    Player = 1,
    Enemy = 2,
    Boss = 3,
    NPC = 4,
}

public enum SoNguyen
{
    Zero = 0,
    One = 1,
    Two = 2,
    Three = 3,
    // ....
}

public enum EquipmentType
{
    Body = 1,
    Sword = 2,
    Shield = 3,
    Quiver = 5,
    Dagger = 6,
    Bow = 4,
    Boot = 7,
    Gloves = 8,
    Helmet = 9,
}