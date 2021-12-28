using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Workshop_3 : MonoBehaviour
{
    [SerializeField] private HealthBarScript healthBar;
    void Start()
    {
        healthBar.InitHealthBar(300);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            healthBar.ChangeLife(-30);
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            healthBar.ChangeLife(45);
            return;
        }
    }
}
