using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PikachuButtonScript : MonoBehaviour
{
    [SerializeField] private Button selfButton;
    [SerializeField] private int pokemonIndex;
    private Vector2Int buttonPos;
    void Start()
    {
        selfButton.onClick.AddListener(OnClickButton);
    }

    public void SetButtonInfo(Vector2Int buttonPosition)
    {
        buttonPos = buttonPosition;
    }

    private void OnClickButton()
    {
        GameManager.instance.OnInteractingPikachuButton(buttonPos, pokemonIndex);
    }
}
