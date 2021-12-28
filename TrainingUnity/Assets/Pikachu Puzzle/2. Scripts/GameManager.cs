using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public const float maxLife = 100;

    [SerializeField] private Text remainTimeText;
    [SerializeField] private Text levelText;
    [SerializeField] private Slider remainTimeSlider;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        int level = 1;
        SpawnGameByLevel(level);
        levelText.text = $"Level: {level}"; // string.Format("Level: {0}", level);
        remainTime = levelTime;
        SetRemainTimeText((int)remainTime);
        SetRemainTimeSlider();
    }

    float levelTime = 300;
    float tickTimer = 0;
    float remainTime;
    private void Update()
    {
        if (isEndGame == true) return;
        tickTimer += Time.deltaTime;
        if (tickTimer >= 1)
        {
            tickTimer--;
            SetRemainTimeText((int)remainTime);
        }
        remainTime -= Time.deltaTime;
        SetRemainTimeSlider();
        if (remainTime <= 0)
        {
            ShowEndGamePopup(false);
        }
    }

    private void SetRemainTimeText(int RemainTime)
    {
        remainTimeText.text = string.Format("Remain Time: {0:D2}:{1:D2}", RemainTime / 60, RemainTime % 60);
    }
    private void SetRemainTimeSlider()
    {
        remainTimeSlider.value = remainTime / levelTime;
    }

    #region MATCHING
    bool isFirstTouch = false;
    public void OnInteractingPikachuButton(Vector2Int buttonPos, int pokemonIndex)
    {
        isFirstTouch = !isFirstTouch;
        if (isFirstTouch == false)
        {
            if (CheckMatchWithCurrentPokemon(buttonPos, pokemonIndex))
            {
                StartCoroutine(MatchObjectCoroutine(new Vector2Int[4] { firstPos, matchPos_1, matchPos_2, buttonPos }));
            }
        }
        else
        {
            RegisterFirstTouchedPokemon(buttonPos, pokemonIndex);
        }
    }

    int currentPokemonIndex; // index cua pokemon dang duoc tuong tac
    Vector2Int firstPos; // vi tri cua nút đầu tiên được tương tác trên mảng 2 chiều
    Vector2Int secondPos; // vi tri cua nút đầu tiên được tương tác trên mảng 2 chiều
    private void RegisterFirstTouchedPokemon(Vector2Int buttonPos, int pokemonIndex)
    {
        currentPokemonIndex = pokemonIndex;
        firstPos = buttonPos;
    }

    // de sau lam lai trong truong hop noi khac hang
    private Vector2Int matchPos_1, matchPos_2;
    int max1, min1, max2, min2, min, max;
    private bool CheckMatchWithCurrentPokemon(Vector2Int buttonPos, int pokemonIndex)
    {
        if (buttonPos == firstPos) return false;

        if (pokemonIndex != currentPokemonIndex) return false;
        secondPos = buttonPos;

        // check match theo chieu` ngang
        //                           x
        //                           |
        //   -------------------------
        //   |
        //   | 
        //   x

        // check khoang trong tren va duoi button 1
        SetMinMax_Horizontal();
        if (CheckMatch_Horizontal())
        {
            return true;
        }
        // check match theo chieu` doc
        //               ------------x
        //               |            
        //               |
        //               |
        //               | 
        //   x ----------
        SetMinMax_Vertical();
        if (CheckMatch_Vertical())
        {
            return true;
        }


        return false;
    }

    // tinh min, max cho truong hop match theo chieu ngang
    private void SetMinMax_Horizontal()
    {
        max1 = firstPos.y;
        min1 = firstPos.y;
        max2 = secondPos.y;
        min2 = secondPos.y;
        for (int i = firstPos.y + 1; i < 10; i++)
        {
            if (pokemonPosArray[firstPos.x, i] == -1)
            {
                max1 = i;
            }
            else
            {
                break;
            }
        }
        for (int i = firstPos.y - 1; i >= 0; i--)
        {
            if (pokemonPosArray[firstPos.x, i] == -1)
            {
                min1 = i;
            }
            else
            {
                break;
            }
        }

        for (int i = secondPos.y + 1; i < 10; i++)
        {
            if (pokemonPosArray[secondPos.x, i] == -1)
            {
                max2 = i;
            }
            else
            {
                break;
            }
        }
        for (int i = secondPos.y - 1; i >= 0; i--)
        {
            if (pokemonPosArray[secondPos.x, i] == -1)
            {
                min2 = i;
            }
            else
            {
                break;
            }
        }

        min = Mathf.Max(min1, min2);
        max = Mathf.Min(max1, max2);
        Debug.LogError($"min = {min}");
        Debug.LogError($"max = {max}");
    }

    private bool CheckMatch_Horizontal()
    {
        bool canMatch = true;
        if (min > max) return false;
        if (firstPos.x < secondPos.x)
        {
            for (int i = min; i <= max; i++)
            {
                matchPos_1 = new Vector2Int(firstPos.x, i);
                matchPos_2 = new Vector2Int(secondPos.x, i);
                canMatch = true;
                for (int j = firstPos.x; j <= secondPos.x; j++)
                {
                    if (pokemonPosArray[j, i] != -1
                        && !(i == firstPos.y && j == firstPos.x)
                        && !(i == secondPos.y && j == secondPos.x))
                    {
                        canMatch = false;
                    }
                }
                if (canMatch == true)
                {
                    break;
                }
            }
        }
        else if (firstPos.x > secondPos.x)
        {
            for (int i = min; i <= max; i++)
            {
                matchPos_1 = new Vector2Int(firstPos.x, i);
                matchPos_2 = new Vector2Int(secondPos.x, i);
                canMatch = true;
                for (int j = secondPos.x; j <= firstPos.x; j++)
                {
                    if (pokemonPosArray[j, i] != -1
                        && !(i == firstPos.y && j == firstPos.x)
                        && !(i == secondPos.y && j == secondPos.x))
                    {
                        canMatch = false;
                    }
                }
                if (canMatch == true)
                {
                    break;
                }
            }
        }
        if (canMatch == true)
        {
            return true;
        }
        if (max == 9)
        {
            matchPos_1 = new Vector2Int(firstPos.x, 10);
            matchPos_2 = new Vector2Int(secondPos.x, 10);
            return true;
        }
        if (min == 0)
        {
            matchPos_1 = new Vector2Int(firstPos.x, -1);
            matchPos_2 = new Vector2Int(secondPos.x, -1);
            return true;
        }
        return false;
    }

    // tinh min,max cho truong hop match theo chieu doc
    private void SetMinMax_Vertical()
    {
        max1 = firstPos.x;
        min1 = firstPos.x;
        max2 = secondPos.x;
        min2 = secondPos.x;
        for (int i = firstPos.x + 1; i < 12; i++)
        {
            if (pokemonPosArray[i, firstPos.y] == -1)
            {
                max1 = i;
            }
            else
            {
                break;
            }
        }
        for (int i = firstPos.x - 1; i >= 0; i--)
        {
            if (pokemonPosArray[i, firstPos.y] == -1)
            {
                min1 = i;
            }
            else
            {
                break;
            }
        }

        for (int i = secondPos.x + 1; i < 12; i++)
        {
            if (pokemonPosArray[i, secondPos.y] == -1)
            {
                max2 = i;
            }
            else
            {
                break;
            }
        }
        for (int i = secondPos.x - 1; i >= 0; i--)
        {
            if (pokemonPosArray[i, secondPos.y] == -1)
            {
                min2 = i;
            }
            else
            {
                break;
            }
        }

        min = Mathf.Max(min1, min2);
        max = Mathf.Min(max1, max2);
        Debug.LogError($"min = {min}");
        Debug.LogError($"max = {max}");
    }

    private bool CheckMatch_Vertical()
    {
        bool canMatch = true;
        if (min > max) return false;
        if (firstPos.y < secondPos.y)
        {
            for (int i = min; i <= max; i++)
            {
                matchPos_1 = new Vector2Int(i, firstPos.y);
                matchPos_2 = new Vector2Int(i, secondPos.y);
                canMatch = true;
                for (int j = firstPos.y; j <= secondPos.y; j++)
                {
                    if (pokemonPosArray[i, j] != -1
                        && !(i == firstPos.x && j == firstPos.y)
                        && !(i == secondPos.x && j == secondPos.y))
                    {
                        canMatch = false;
                    }
                }
                if (canMatch == true)
                {
                    break;
                }
            }
        }
        else if (firstPos.y > secondPos.y)
        {
            for (int i = min; i <= max; i++)
            {
                matchPos_1 = new Vector2Int(i, firstPos.y);
                matchPos_2 = new Vector2Int(i, secondPos.y);
                canMatch = true;
                for (int j = secondPos.y; j <= firstPos.y; j++)
                {
                    if (pokemonPosArray[i, j] != -1
                        && !(i == firstPos.x && j == firstPos.y)
                        && !(i == secondPos.x && j == secondPos.y))
                    {
                        canMatch = false;
                    }
                }
                if (canMatch == true)
                {
                    break;
                }
            }
        }
        if (canMatch == true)
        {
            return true;
        }
        if (max == 11)
        {
            matchPos_1 = new Vector2Int(12, firstPos.y);
            matchPos_2 = new Vector2Int(12, secondPos.y);
            return true;
        }
        if (min == 0)
        {
            matchPos_1 = new Vector2Int(-1, firstPos.y);
            matchPos_2 = new Vector2Int(-1, secondPos.y);
            return true;
        }
        return false;
    }
    #endregion MATCHING


    #region SPAWN LEVEL
    int totalMatch = 60;// tổng số cặp của level
    int[,] dataLevel_1 = new int[12, 10]
    {
        { 1,2,1,3,4,7,9,10,1,2},
        { 3,4,7,8,8,9,7,10,7,3},
        { 5,4,3,2,1,5,7,9,10,6},
        { 8,2,7,4,3,1,3,2,6,9},
        { 10,7,8,5,4,3,6,8,1,1},
        { 8,2,4,5,8,6,8,9,10,8},
        { 1,1,1,1,1,1,1,1,1,1},
        { 7,7,7,7,7,7,7,7,7,7},
        { 4,4,4,4,4,4,4,4,4,4},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1},
        { 1,1,1,1,1,1,1,1,1,1}
    };



    // mang 2 chieu chua cac gia tri la pokemonIndex
    // neu vi tri do bi trong thi gia tri la -1
    int[,] pokemonPosArray;
    PikachuButtonScript[,] pokemonButtonArray;
    private void SpawnGameByLevel(int level)
    {
        pokemonPosArray = new int[arrayWidth, arrayHeight];
        pokemonButtonArray = new PikachuButtonScript[arrayWidth, arrayHeight];
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int randomIndex = dataLevel_1[i, j];//j;//Random.Range(1, 11);
                pokemonPosArray[i, j] = randomIndex;
                pokemonButtonArray[i, j] = SpawnPokemonInPosition(new Vector2Int(i, j), randomIndex);
            }
        }
    }



    [SerializeField] private List<PikachuButtonScript> pokemonPrefabList;
    [SerializeField] private Transform gameplayTransform;
    PikachuButtonScript currentSpawnedPrefab;
    /// <summary>
    /// Spawn 1 pokemon tai vi tri tren mang 2 chieu
    /// </summary>
    /// <param name="buttonPos"></param>
    /// <param name="pokemonIndex"></param>
    private PikachuButtonScript SpawnPokemonInPosition(Vector2Int buttonPos, int pokemonIndex)
    {
        if (pokemonIndex < 1 || pokemonIndex > pokemonPrefabList.Count)
        {
            return null;
        }

        currentSpawnedPrefab = Instantiate(pokemonPrefabList[pokemonIndex - 1], gameplayTransform);
        currentSpawnedPrefab.GetComponent<RectTransform>().anchoredPosition = GetAnchorPos(buttonPos);
        currentSpawnedPrefab.SetButtonInfo(buttonPos);
        return currentSpawnedPrefab;
    }

    // settings
    float buttonWidth = 60;
    float buttonHeight = 60;
    int arrayWidth = 12;
    int arrayHeight = 10;
    float offsetY = -50;
    /// <summary>
    /// Tinh vi tri cua 1 button theo vi tri tren mang 2 chieu
    /// </summary>
    /// <param name="buttonPos"></param>
    /// <returns></returns>
    private Vector2 GetAnchorPos(Vector2Int buttonPos)
    {
        Vector2 positionAt_Zero_Zero = new Vector2(60 * (-(float)arrayWidth / 2 + 0.5f),
            60 * (-(float)arrayHeight / 2 + 0.5f) + offsetY);
        return positionAt_Zero_Zero + new Vector2(60 * buttonPos.x, 60 * buttonPos.y);
    }
    #endregion SPAWN LEVEL

    #region SPAWN LINE AND DESTROY MATCH OBJECTS
    IEnumerator MatchObjectCoroutine(Vector2Int[] poss)
    {
        totalMatch--;
        SpawnConnectedLine(poss);
        yield return new WaitForSeconds(1);
        DestroyPokemonObject(poss[0]);
        DestroyPokemonObject(poss[poss.Length - 1]);
        HideConnectedLine();
        if (CheckVictory())
        {
            ShowEndGamePopup(true);
        }
    }
    [SerializeField] private LineRenderer connectedLine;
    private void SpawnConnectedLine(Vector2Int[] poss)
    {
        connectedLine.positionCount = poss.Length;
        for (int i = 0; i < connectedLine.positionCount; i++)
        {
            connectedLine.SetPosition(i, GetTransformPos(poss[i]));
        }
    }

    private void HideConnectedLine()
    {
        connectedLine.positionCount = 0;
    }

    private bool CheckVictory()
    {
        // check so cap button con lai
        // neu bang 0 => victory
        // nguoc lai thi chua thang game
        if (totalMatch <= 0)
        {
            return true;
        }
        return false;
    }
    private void DestroyPokemonObject(Vector2Int buttonPos)
    {
        pokemonPosArray[buttonPos.x, buttonPos.y] = -1;
        Destroy(pokemonButtonArray[buttonPos.x, buttonPos.y].gameObject);
    }


    private Vector2 GetTransformPos(Vector2Int buttonPos)
    {
        // lay vi tri theo canvas
        Vector2 anchorPos = GetAnchorPos(buttonPos);
        // chuyen sang vi tri theo transform
        return new Vector2(anchorPos.x / 72, anchorPos.y / 72);
    }
    #endregion

    #region SHOW END GAME POPUP
    private bool isEndGame = false;
    [SerializeField] private EndGamePopup endGamePopup;
    private void ShowEndGamePopup(bool isVictory)
    {
        isEndGame = true;
        //Time.timeScale = 0;
        int score = (int)remainTime * 100;
        endGamePopup.ShowEndGamePopup(isVictory, remainTime, score, levelText.text);
    }
    #endregion
}
