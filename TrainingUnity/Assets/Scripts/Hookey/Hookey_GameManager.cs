using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hookey_GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Rigidbody2D ball;
    private int playerScore = 0;
    private int enemyScore = 0;

    public static Hookey_GameManager instance;

    [SerializeField] private Button playAgainButton, exitGameButton;
    [SerializeField] private GameObject endGamePopup;

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

        playAgainButton.onClick.AddListener(PlayAgain);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    public void NewGame()
    {
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

    private void PlayAgain()
    {
        Debug.LogError("nhan nut play again");
        endGamePopup.SetActive(false);
    }

    private void ExitGame()
    {
        Debug.LogError("nhan nut exit game");
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
