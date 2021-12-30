using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    // item consumable: hồi máu, mana, thức ăn (có thể có cấp độ hoặc ko)
    // Life_Flask
    // Mana_Flask
    // Food
    // tier 0: none, tier 1: small, tier 2: large, tier 3: grand, tier 4: ...

    // vũ khí, quần áo, giáp ... (có các cấp độ)
    // sword
    // bow
    // dagger
    // claw
    // axe
    // scepter
    // staff
    // damage
    // equip type: 1 hand, 2 hand...
    // rarity: normal - magic - rare - unique - heroic - legendary
    // Level: 1,2,3,...
    // damage
    // durability


    // body armour
    // helmet
    // gloves
    // boot
    // armour



    // quest item
    // item name ...
    // xp reward, gold reward
    // quest name (phục vụ cho nhiệm vụ nào, bước thứ mấy của nhiệm vụ)



    // craft material (có các cấp độ rarity, level)
    // blood stone
    // moon stone
    // 

    // charm (diablo)
    // name
    // level
    // ...

    //


    public string ItemName; // tên của item
    public InventoryItemType ItemType;
    public string ItemData; // chính là json lưu data của item



    


    // ItemName = Life Flask
    // ItemType = InventoryItemType.LifeFlask
    // ItemTier = 2
    // Damage = -1
    // Armour = -1
    // {ItemTier = 2}


    // ItemName = Sword
    // ItemType = InventoryItemType.Sword
    // ItemTier = 0
    // Damage = 101
    // Armour = -1
    // {EquipType = OneHand, Level = 25, Damage = 101,....}
}

public enum InventoryItemType
{
    LifeFlask = 1,
    ManaFlask = 2,
    Food = 3,

    Sword = 4,
    Dagger = 5,

    BodyArmour = 6,
    Gloves = 7,

    QuestItem = 8,

    CraftMaterial = 9,

    Charm = 10
}