using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hookey_EndGamePopup : MonoBehaviour
{
    [SerializeField] private Text titleText; // YOU WIN / YOU LOSE
    [SerializeField] private Button playAgainButton, exitGameButton;
    
    void Start()
    {
        Debug.LogError("Start");
        playAgainButton.onClick.AddListener(PlayAgain);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void OnEnable()
    {
        Debug.LogError("OnEnable");
    }

    /// <summary>
    /// Hiển thị popup end game khi người chơi thắng hoặc thua
    /// </summary>
    /// <param name="IsPlayerWin">True nếu như người chơi thắng, false nếu ngược lại</param>
    public void ShowEndGame(bool IsPlayerWin)
    {
        gameObject.SetActive(true);
        if(IsPlayerWin)
        {
            titleText.text = "YOU WIN !!!";
        }
        else
        {
            titleText.text = "YOU LOSE !!!";
        }
    }

    private void PlayAgain()
    {
        Debug.LogError("nhan nut play again");
        Hookey_GameManager.instance.NewGame();
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        Debug.LogError("nhan nut exit game");
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
