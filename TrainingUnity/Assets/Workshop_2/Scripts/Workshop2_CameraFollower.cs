using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop2_CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float safeOffset; // khoảng cách an toàn cho người di chuyển bên trong camera


    private float x;
    void Update()
    {
        // Vị trí người chơi trừ đi cho vị trí của camera ở tại frame
        x = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(x) > safeOffset)
        {
            transform.position = new Vector3(player.position.x - Mathf.Sign(x) * (safeOffset - 0.1f), transform.position.y, transform.position.z);
        }
    }
}