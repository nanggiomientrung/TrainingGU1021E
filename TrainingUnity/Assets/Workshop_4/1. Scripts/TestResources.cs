using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestResources : MonoBehaviour
{
    [SerializeField] private Button loadButton_1;
    [SerializeField] private Button loadButton_2;
    [SerializeField] private Image image_1, image_2;

    void Start()
    {
        loadButton_1.onClick.AddListener(LoadResource_File_1);
        loadButton_2.onClick.AddListener(LoadResource_File_2);
    }

    private void LoadResource_File_1()
    {
        image_1.sprite = Resources.Load<Sprite>("Layer 1");
        image_1.SetNativeSize();
    }
    private void LoadResource_File_2()
    {
        //image_2.sprite = Resources.Load<Sprite>("Test/Test_2");
        image_2.sprite = Resources.Load<Sprite>("Layer 2");
        image_2.SetNativeSize();
    }
}
