using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIScript : MonoBehaviour
{
    [SerializeField] private Transform rect_1, rect_2, rect_3, rect_4;
    void Start()
    {
        //rect_2.SetAsFirstSibling();
        //rect_1.SetAsLastSibling();

        Debug.LogError("chieu rong man hinh: " + Screen.width);
        Debug.LogError("chieu cao man hinh: " + Screen.height);
    }
}