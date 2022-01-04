using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCreator : MonoBehaviour
{
    [SerializeField] private Button insertItemButton;
    [SerializeField] private Button genJsonButton;
    [SerializeField] private Button retrieveInventoryButton;

    [SerializeField] private Text text_2, text_3, text_4, text_5;
    [SerializeField] private TMP_Dropdown itemTypeDropDown;
    [SerializeField] private TMP_Dropdown rarityDropDown;
    [SerializeField] private TMP_InputField inputField_1, inputField_2, inputField_3, inputField_ItemName;


    InventoryItemList items = new InventoryItemList();
    InventoryItem currentInserItem;
    InventoryItemType currentItemType = InventoryItemType.LifeFlask;
    private void Start()
    {
        insertItemButton.onClick.AddListener(InsertItem);
        genJsonButton.onClick.AddListener(GenJson);
        retrieveInventoryButton.onClick.AddListener(RetrieveData);

        itemTypeDropDown.onValueChanged.AddListener(OnItemTypeChange);
    }

    private void OnItemTypeChange(int value)
    {
        switch (value)
        {
            case 0:
            case 1:
            case 2:
                // hiển thị 3 ô đầu là ItemTier;StackQuantity;Quantity;
                text_2.text = "Item Tier";
                text_3.text = "Stack Qty";
                text_4.text = "Qty";
                text_5.gameObject.SetActive(false);
                rarityDropDown.gameObject.SetActive(false);
                break;
            case 3:
            case 4:
                //public ItemRarity Rarity;
                //public int Damage;
                //public int Level;
                //public int Durability;
                text_2.text = "Damage";
                text_3.text = "Level";
                text_4.text = "Durability";
                text_5.gameObject.SetActive(true);
                rarityDropDown.gameObject.SetActive(true);
                break;
            case 5:
            case 6:
                //public ItemRarity Rarity;
                //public int ArmourValue;
                //public int Level;
                //public int Durability;
                text_2.text = "ArmourValue";
                text_3.text = "Level";
                text_4.text = "Durability";
                text_5.gameObject.SetActive(true);
                rarityDropDown.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        currentItemType = (InventoryItemType)(value + 1); // (float)5 => 5f
    }

    private void InsertItem()
    {
        currentInserItem = new InventoryItem();

        currentInserItem.ItemName = inputField_ItemName.text;
        currentInserItem.ItemType = currentItemType;

        switch (currentItemType)
        {
            case InventoryItemType.LifeFlask:
                LifeFlask tempLifeFlask = new LifeFlask();
                tempLifeFlask.ItemTier = int.Parse(inputField_1.text);
                tempLifeFlask.StackQuantity = int.Parse(inputField_2.text);
                tempLifeFlask.Quantity = int.Parse(inputField_3.text);

                currentInserItem.ItemData = JsonUtility.ToJson(tempLifeFlask);
                break;
            case InventoryItemType.ManaFlask:
                ManaFlask tempManaFlask = new ManaFlask();
                tempManaFlask.ItemTier = int.Parse(inputField_1.text);
                tempManaFlask.StackQuantity = int.Parse(inputField_2.text);
                tempManaFlask.Quantity = int.Parse(inputField_3.text);

                currentInserItem.ItemData = JsonUtility.ToJson(tempManaFlask);
                break;
            case InventoryItemType.Food:
                Food tempFood = new Food();
                tempFood.ItemTier = int.Parse(inputField_1.text);
                tempFood.StackQuantity = int.Parse(inputField_2.text);
                tempFood.Quantity = int.Parse(inputField_3.text);

                currentInserItem.ItemData = JsonUtility.ToJson(tempFood);
                break;
            case InventoryItemType.Sword:
            case InventoryItemType.Dagger:
                Weapon tempWeapon = new Weapon();
                tempWeapon.Damage = int.Parse(inputField_1.text);
                tempWeapon.Level = int.Parse(inputField_2.text);
                tempWeapon.Durability = int.Parse(inputField_3.text);
                tempWeapon.Rarity = (ItemRarity)rarityDropDown.value;
                currentInserItem.ItemData = JsonUtility.ToJson(tempWeapon);
                break;
            case InventoryItemType.BodyArmour:
            case InventoryItemType.Gloves:
                ArmourItem tempArmour = new ArmourItem();
                tempArmour.ArmourValue = int.Parse(inputField_1.text);
                tempArmour.Level = int.Parse(inputField_2.text);
                tempArmour.Durability = int.Parse(inputField_3.text);
                tempArmour.Rarity = (ItemRarity)rarityDropDown.value;
                currentInserItem.ItemData = JsonUtility.ToJson(tempArmour);
                break;
            default:
                break;
        }

        items.ItemList.Add(currentInserItem);
    }

    private void GenJson()
    {
        //Debug.LogError(JsonUtility.ToJson(items));
        SaveDataToPersistent("InventoryData", JsonUtility.ToJson(items));
    }

    private void SaveDataToPersistent(string fileName, string dataString)
    {
        string dataPath = $"{Application.persistentDataPath}/{fileName}.txt";
        new System.Threading.Thread(() =>
        {
            File.WriteAllText(dataPath, dataString);
        }
        ).Start();
    }

    private void RetrieveData()
    {
        string dataString = ReadDataFromPersistent("InventoryData");
        if(dataString != "")
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
}
