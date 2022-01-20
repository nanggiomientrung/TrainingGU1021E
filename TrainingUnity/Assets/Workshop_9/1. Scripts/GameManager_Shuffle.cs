using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager_Shuffle : MonoBehaviour
{
    [SerializeField] private List<Transform> upPointList_ForLeft; // di chuyển lên cho xô bên trái
    [SerializeField] private List<Transform> downPointList_ForLeft; // di chuyển xuống cho xô bên trái
    [SerializeField] private List<Transform> horizontalPointList_ForLeft; // di chuyển ngang cho xô bên trái

    [SerializeField] private List<Transform> upPointList_ForRight;
    [SerializeField] private List<Transform> downPointList_ForRight;
    [SerializeField] private List<Transform> horizontalPointList_ForRight;


    [SerializeField] private Button runButton_Up, runButton_Down, runButton_Horizontal;

    private Dictionary<DirectrionEnum, int> shoveDict = new Dictionary<DirectrionEnum, int>();

    [SerializeField] private Transform[] shoves;


    private void Start()
    {
        runButton_Up.onClick.AddListener(MoveUp);
        runButton_Down.onClick.AddListener(MoveDown);
        runButton_Horizontal.onClick.AddListener(MoveHorizontal);
        shoveDict.Add(DirectrionEnum.Left, 0);
        shoveDict.Add(DirectrionEnum.Right, 1);
    }

    private void MoveUp()
    {
        shoves[shoveDict[DirectrionEnum.Left]].DOPath(new Vector3[4]
            {
                upPointList_ForLeft[0].position,
                upPointList_ForLeft[1].position,
                upPointList_ForLeft[2].position,
                upPointList_ForLeft[3].position
            }, 0.5f);

        shoves[shoveDict[DirectrionEnum.Right]].DOPath(new Vector3[4]
            {
                downPointList_ForRight[0].position,
                downPointList_ForRight[1].position,
                downPointList_ForRight[2].position,
                downPointList_ForRight[3].position
            }, 0.5f).OnComplete(SwapShove);
    }

    private void MoveDown()
    {
        shoves[shoveDict[DirectrionEnum.Left]].DOPath(new Vector3[4]
            {
                downPointList_ForLeft[0].position,
                downPointList_ForLeft[1].position,
                downPointList_ForLeft[2].position,
                downPointList_ForLeft[3].position
            }, 0.5f);

        shoves[shoveDict[DirectrionEnum.Right]].DOPath(new Vector3[4]
            {
                upPointList_ForRight[0].position,
                upPointList_ForRight[1].position,
                upPointList_ForRight[2].position,
                upPointList_ForRight[3].position
            }, 0.5f).OnComplete(SwapShove);
    }
    private void MoveHorizontal()
    {
        shoves[shoveDict[DirectrionEnum.Left]].DOPath(new Vector3[2]
               {
                horizontalPointList_ForLeft[0].position,
                horizontalPointList_ForLeft[1].position
               }, 0.25f);

        shoves[shoveDict[DirectrionEnum.Right]].DOPath(new Vector3[2]
            {
                horizontalPointList_ForRight[0].position,
                horizontalPointList_ForRight[1].position
            }, 0.25f).OnComplete(SwapShove);

    }

    int tempShoveIndex;
    private void SwapShove()
    {
        tempShoveIndex = shoveDict[DirectrionEnum.Left];
        shoveDict[DirectrionEnum.Left] = shoveDict[DirectrionEnum.Right];
        shoveDict[DirectrionEnum.Right] = tempShoveIndex;
    }
}

public enum DirectrionEnum
{
    Left,
    Right,
}