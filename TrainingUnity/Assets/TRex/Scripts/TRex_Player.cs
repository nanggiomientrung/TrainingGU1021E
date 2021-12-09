using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex_Player : MonoBehaviour
{
    private bool isOnGround = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isOnGround)
        {
            transform.DOMoveY(-0.5f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(ResetOnGround);
        }
    }

    private void ResetOnGround()
    {
        isOnGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TRex_GameManager.instance.GameOver();
    }
}
