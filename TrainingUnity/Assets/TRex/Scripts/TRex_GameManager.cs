using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TRex_GameManager : MonoBehaviour
{
    [SerializeField] private LevelData_ScriptableObject levelDataScriptableObject;
    [SerializeField] private float cooldown = 2;

    private float score;
    [SerializeField] private float scoreByTimeMultiplier = 1;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Button replayButton;

    public static TRex_GameManager instance;

    private void Awake()
    {
        instance = this;
        replayButton.onClick.AddListener(RestartGame);

        highScoreText.text = $"High score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    private void Start()
    {
        StartCoroutine(AutoSpawnObstacleCoroutine());
    }

    //private float timer = 0;
    private void Update()
    {
        //timer += Time.deltaTime;
        //if(timer >= cooldown)
        //{
        //    timer -= cooldown;
        //    SpawnObstacle();
        //    cooldown = Random.Range(0.5f, 2.5f);
        //}

        score += Time.deltaTime * scoreByTimeMultiplier;

        scoreText.text = $"SCORE: {(int)score}";
    }

    private void SpawnObstacle()
    {
        GameObject temp;
        if (score <= 100)
        {
            temp = Instantiate(levelDataScriptableObject.levelData[0].prefab);
            temp.GetComponent<TRex_Obstacle>().SetVelocity(levelDataScriptableObject.levelData[0].velocity);
        }
        else
        {
            if (score <= 200)
            {
                temp = Instantiate(levelDataScriptableObject.levelData[1].prefab);
                temp.GetComponent<TRex_Obstacle>().SetVelocity(levelDataScriptableObject.levelData[1].velocity);
            }
            else
            {
                temp = Instantiate(levelDataScriptableObject.levelData[2].prefab);
                temp.GetComponent<TRex_Obstacle>().SetVelocity(levelDataScriptableObject.levelData[2].velocity);
            }
        }

        temp.transform.position = new Vector3(12, -3, 0);
        obstacleList.Add(temp);
    }

    IEnumerator AutoSpawnObstacleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            SpawnObstacle();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        replayButton.gameObject.SetActive(true);

        if ((int)score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
        }
    }

    private List<GameObject> obstacleList = new List<GameObject>();

    private void RestartGame()
    {
        replayButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        score = 0;
        for (int i = 0; i < obstacleList.Count; i++)
        {
            Destroy(obstacleList[i]);
        }
        obstacleList.Clear();

        highScoreText.text =$"High score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }



}
