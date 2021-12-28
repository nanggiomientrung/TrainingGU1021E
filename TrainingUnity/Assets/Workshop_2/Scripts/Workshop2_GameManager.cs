using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop2_GameManager : MonoBehaviour
{
    public static Workshop2_GameManager instance;
    private bool isPauseGame = false;

    private void Awake()
    {
        instance = this;
        StartCoroutine(AutoSpawnFrog());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPauseGame)
            {
                // resume game
                isPauseGame = false;
                Debug.LogError("Resume game");

                if (OnResumeGame != null)
                {
                    OnResumeGame();
                }
            }
            else
            {
                // pasue game
                isPauseGame = true;
                Debug.LogError("Pause game");

                if (OnPauseGame != null)
                {
                    OnPauseGame("hey hey");
                }
            }
        }
    }

    public Action<string> OnPauseGame;
    public Action OnResumeGame;


    IEnumerator AutoSpawnFrog()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (isPauseGame == false)
            {
                SpawnFrog();
            }
        }
    }

    [SerializeField] private GameObject frogPrefab;
    private GameObject tempFrog;
    private void SpawnFrog()
    {
        tempFrog = SimplePool.Spawn(frogPrefab);
        tempFrog.transform.position = new Vector3(8.8f, UnityEngine.Random.Range(-2f, 2), -1);

    }

    [SerializeField] private Sprite testSprite;
}
