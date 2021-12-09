using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong_GameManager : MonoBehaviour
{
    [SerializeField] private TextMesh scoreText;
    private int playerScore;
    private int enemyScore;

    public static PingPong_GameManager instance;
    // singleton design pattern

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // giu~ game object sau khi chuyen scene
    }

    /// <summary>
    /// Thay đổi điểm số khi bóng chạm đất <br/>
    /// truyền vào TRUE nếu như player giành điểm <br/>
    /// truyền vào FALSE nếu như enemy giành điểm
    /// </summary>
    /// <param name="IsPlayerWin"></param>
    public void ChangeScore(bool IsPlayerWin)
    {
        if (IsPlayerWin)
        {
            playerScore++;
            StartCoroutine(NewTurnCoroutine(false));
            //NewTurn(false);
        }
        else
        {
            enemyScore++;
            StartCoroutine(NewTurnCoroutine(true));
            //NewTurn(true);
        }

        scoreText.text = $"{playerScore}-{enemyScore}";
    }

    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private Vector2 ballPositionAtPlayerSide;
    [SerializeField] private Vector2 ballPositionAtEnemySide;

    private IEnumerator NewTurnCoroutine(bool IsPlayerSide)
    {
        ball.velocity = Vector2.zero;
        ball.bodyType = RigidbodyType2D.Kinematic;
        if (IsPlayerSide)
        {
            ball.transform.position = ballPositionAtPlayerSide;
        }
        else
        {
            ball.transform.position = ballPositionAtEnemySide;
        }
        yield return new WaitForSeconds(2);
        ball.bodyType = RigidbodyType2D.Dynamic;
    }
    private void NewTurn(bool IsPlayerSide)
    {
        ball.velocity = Vector2.zero;
        if (IsPlayerSide)
        {
            ball.transform.position = ballPositionAtPlayerSide;
        }
        else
        {
            ball.transform.position = ballPositionAtEnemySide;
        }
    }
}