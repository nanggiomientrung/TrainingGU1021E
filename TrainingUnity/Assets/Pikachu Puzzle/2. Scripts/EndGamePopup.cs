using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] private Text levelText, remainTimeText, scoreText, resultText;
    [SerializeField] private GameObject greyBackground;
    [SerializeField] private Transform popup;
    public void ShowEndGamePopup(bool isVictory, float remainTime, int score, string level)
    {
        levelText.text = level;
        remainTimeText.text = string.Format("Remain Time: {0:D2}:{1:D2}", (int)remainTime / 60, (int)remainTime % 60);
        scoreText.text = string.Format("Score: {0}", score);
        greyBackground.SetActive(true);
        popup.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuart);
        if(isVictory)
        {
            resultText.text = "YOU WIN !";
        }
        else
        {
            resultText.text = "YOU LOSE !";
        }
    }
}
