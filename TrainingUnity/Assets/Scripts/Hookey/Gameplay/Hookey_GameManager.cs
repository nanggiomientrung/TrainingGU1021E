using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hookey_GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private Hookey_EndGamePopup endGamePopup;

    private int playerScore = 0;
    private int enemyScore = 0;

    public static Hookey_GameManager instance;

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);

        ResetBallAfterGoal();
    }

    public void NewGame()
    {
        ball.bodyType = RigidbodyType2D.Dynamic;
        playerScore = 0;
        enemyScore = 0;
        DisplayScoreText();
    }
    public void ChangeScore(bool IsPlayerWin)
    {
        if(IsPlayerWin)
        {
            playerScore++;
        }
        else
        {
            enemyScore++;
        }
        ResetBallAfterGoal();
        DisplayScoreText();

        if (playerScore == 5 || enemyScore == 5)
        {
            endGamePopup.ShowEndGame(playerScore == 5);
            ball.bodyType = RigidbodyType2D.Static;
        }
    }

    private void DisplayScoreText()
    {
        scoreText.text = $"{playerScore}-{enemyScore}";
    }

    private void ResetBallAfterGoal()
    {
        ball.transform.position = new Vector3(0, -2, 0);
        //ball.velocity = new Vector2(0,-8);
        ball.velocity = Vector2.zero;
    }
}
