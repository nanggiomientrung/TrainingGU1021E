using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemScript : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Button selfButton;
    [SerializeField] private GameObject selectGO;
    private string itemName; // tên của item
    private InventoryItemType itemType;
    private string itemData; // chính là json lưu data của item

    private int itemIndex;

    private void Start()
    {
        selfButton.onClick.AddListener(SelectItem);
        InventoryController.instance.OnSelectItem += OnSelectItem;
    }
    private void OnDestroy()
    {
        InventoryController.instance.OnSelectItem -= OnSelectItem;
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

        if (ItemIndex == 0)
        {
            StartCoroutine(AutoSelectItem());
        }
    }

    private IEnumerator AutoSelectItem()
    {
        yield return null;
        SelectItem();
    }

    private void SelectItem()
    {
        InventoryController.instance.SelectItem(itemIndex, iconImage.sprite);
        selectGO.SetActive(true);
    }

    private void OnSelectItem(int selectedItemIndex)
    {
        if (itemIndex != selectedItemIndex)
        {
            selectGO.SetActive(false);
        }
    }
}
