using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemScript : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Button selfButton;
    private string itemName; // tên của item
    private InventoryItemType itemType;
    private string itemData; // chính là json lưu data của item

    private int itemIndex;

    private void Start()
    {
        selfButton.onClick.AddListener(SelectItem);
    }

    public void SetDataToItem(int ItemIndex, string ItemName, InventoryItemType ItemType, string ItemData)
    {
        itemIndex = ItemIndex;
        itemName = ItemName;
        itemData = ItemData;
        itemType = ItemType;
        switch (ItemType)
        {
            case InventoryItemType.LifeFlask:
                iconImage.sprite = Resources.Load<Sprite>("InventoryIcon/LifeFlask");
                break;
            case InventoryItemType.ManaFlask:
                iconImage.sprite = Resources.Load<Sprite>("InventoryIcon/ManaFlask");
                break;
            case InventoryItemType.Food:
                iconImage.sprite = Resources.Load<Sprite>("InventoryIcon/Food");
                break;
            case InventoryItemType.Dagger:
            case InventoryItemType.Sword:
                iconImage.sprite = Resources.Load<Sprite>("InventoryIcon/Weapon");
                break;
            case InventoryItemType.BodyArmour:
            case InventoryItemType.Gloves:
                iconImage.sprite = Resources.Load<Sprite>("InventoryIcon/Armour");
                break;
        }
    }

    private void SelectItem()
    {
        InventoryController.instance.SelectItem(itemIndex);
    }
}
