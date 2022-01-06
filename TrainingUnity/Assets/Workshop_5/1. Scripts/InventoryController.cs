using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private InventoryItemScript prefab;

    [SerializeField] private Text itemInformationText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button upgradeButton, sellButton;


    public Action<int> OnSelectItem;
    public static InventoryController instance;
    private void Awake()
    {
        instance = this;
    }

    int currentSelectItemIndex = -1;
    public void SelectItem(int itemIndex, Sprite itemIconSprite)
    {
        currentSelectItemIndex = itemIndex;
        if (OnSelectItem != null)
        {
            // bắn event thông báo có item được chọn với index là itemIndex
            OnSelectItem(itemIndex);
        }

        itemIcon.sprite = itemIconSprite;

        InventoryItemType selectedItemType = items.ItemList[itemIndex].ItemType;

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.interactable = true;
        switch (selectedItemType)
        {
            case InventoryItemType.LifeFlask:
            case InventoryItemType.ManaFlask:
            case InventoryItemType.Food:
                ConsumableItem consumableItem = JsonUtility.FromJson<ConsumableItem>(items.ItemList[itemIndex].ItemData);
                itemInformationText.text = string.Format($"{items.ItemList[itemIndex].ItemName}\n" +
                    $"Item Tier: {consumableItem.ItemTier}\nQuantity: {consumableItem.Quantity}/{consumableItem.StackQuantity}");

                upgradeButton.onClick.AddListener(() => { UpgradeConsumableItem(itemIndex, consumableItem); });
                break;
            case InventoryItemType.Dagger:
            case InventoryItemType.Sword:
                Weapon weapon = JsonUtility.FromJson<Weapon>(items.ItemList[itemIndex].ItemData);
                itemInformationText.text = string.Format($"{items.ItemList[itemIndex].ItemName}\n" +
                    $"Damage: {weapon.Damage}\nLevel: {weapon.Level}\nItemRarity: {weapon.Rarity}\nDurability: {weapon.Durability}");
                upgradeButton.onClick.AddListener(() => { UpgradeWeaponItem(itemIndex, weapon); });
                break;
            case InventoryItemType.BodyArmour:
            case InventoryItemType.Gloves:
                ArmourItem armourItem = JsonUtility.FromJson<ArmourItem>(items.ItemList[itemIndex].ItemData);
                itemInformationText.text = string.Format($"{items.ItemList[itemIndex].ItemName}\n" +
                    $"Armour: {armourItem.ArmourValue}\nLevel: {armourItem.Level}\nItemRarity: {armourItem.Rarity}\nDurability: {armourItem.Durability}");
                upgradeButton.onClick.AddListener(() => { UpgradeArmourItem(itemIndex, armourItem); });
                break;
        }
    }

    private InventoryItemScript currentSpawnedItem;
    void Start()
    {
        RetrieveData();

        SpawnListItems();

        sellButton.onClick.AddListener(SellItem);
    }

    private void SpawnListItems()
    {
        for (int i = 0; i < items.ItemList.Count; i++)
        {
            currentSpawnedItem = SimplePool.Spawn(prefab);
            currentSpawnedItem.transform.parent = content;
            currentSpawnedItem.transform.localScale = Vector3.one;

            currentSpawnedItem.SetDataToItem(i, items.ItemList[i].ItemName, items.ItemList[i].ItemType, items.ItemList[i].ItemData);
        }
        if (items.ItemList.Count == 0)
        {
            sellButton.interactable = false;
            upgradeButton.interactable = false;
            itemInformationText.text = "";
        }
    }

    #region MODIFY INVENTORY ITEM
    private void SellItem()
    {
        items.ItemList.RemoveAt(currentSelectItemIndex);
        SaveData(JsonUtility.ToJson(items));

        foreach (Transform item in content)
        {
            SimplePool.Despawn(item.gameObject);
        }
        SpawnListItems();
    }

    private void UpgradeConsumableItem(int itemIndex, ConsumableItem consumableItem)
    {
        consumableItem.ItemTier++;

        items.ItemList[itemIndex].ItemData = JsonUtility.ToJson(consumableItem);
        SaveData(JsonUtility.ToJson(items));

        SelectItem(itemIndex, itemIcon.sprite);
    }
    private void UpgradeWeaponItem(int itemIndex, Weapon weapon)
    {
        weapon.Level++;
        items.ItemList[itemIndex].ItemData = JsonUtility.ToJson(weapon);
        SaveData(JsonUtility.ToJson(items));

        SelectItem(itemIndex, itemIcon.sprite);
    }
    private void UpgradeArmourItem(int itemIndex, ArmourItem armourItem)
    {
        armourItem.Level++;
        items.ItemList[itemIndex].ItemData = JsonUtility.ToJson(armourItem);
        SaveData(JsonUtility.ToJson(items));

        SelectItem(itemIndex, itemIcon.sprite);
    }
    #endregion MODIFY INVENTORY ITEM

    #region LOAD ITEM FROM LOCAL
    InventoryItemList items = new InventoryItemList();
    private void SaveData(string dataString)
    {
        string dataPath = $"{Application.persistentDataPath}/InventoryData.txt";
        File.WriteAllText(dataPath, dataString);
    }

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