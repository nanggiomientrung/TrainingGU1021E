using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private InventoryItemScript prefab;
    [SerializeField] private Text itemInformationText;

    public static InventoryController instance;
    private void Awake()
    {
        instance = this;
    }

    public void SelectItem(int itemIndex)
    {
        itemInformationText.text = string.Format($"{items.ItemList[itemIndex].ItemName}\n{items.ItemList[itemIndex].ItemType}");
    }

    private InventoryItemScript currentSpawnedItem;
    void Start()
    {
        RetrieveData();
        for (int i = 0; i < items.ItemList.Count; i++)
        {
            currentSpawnedItem = Instantiate(prefab, content);

            currentSpawnedItem.SetDataToItem(i, items.ItemList[i].ItemName, items.ItemList[i].ItemType, items.ItemList[i].ItemData);
        }
    }

    #region LOAD ITEM FROM LOCAL
    InventoryItemList items = new InventoryItemList();
    private void RetrieveData()
    {
        string dataString = ReadDataFromPersistent("InventoryData");
        if (dataString != "")
        {
            items = JsonUtility.FromJson<InventoryItemList>(dataString);
        }
        else
        {
            items = new InventoryItemList();
        }
    }

    private string ReadDataFromPersistent(string fileName)
    {
        string dataPath = $"{Application.persistentDataPath}/{fileName}.txt";
        if (System.IO.File.Exists(dataPath))
        {
            return System.IO.File.ReadAllText(dataPath);
        }
        else
        {
            return "";
        }
    }
    #endregion LOAD ITEM FROM LOCAL
}