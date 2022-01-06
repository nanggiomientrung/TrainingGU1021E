using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStatConfigData", menuName = "ItemStatConfig", order = 1)]
public class ItemStatConfig : ScriptableObject
{
    public List<ConsumableConfig> consumableConfigs;
    public Dictionary<ItemRarity, WeaponConfig> weaponConfigs;
    public List<ArmourConfig> armourConfigs;
}

[Serializable]
public class ConsumableConfig
{
    public int BaseValue; // 100
    public int IncreaseValuePerTier; // 20

    public int ComsumeValue(int ItemTier)
    {
        if (ItemTier < 0) ItemTier = 0;
        return BaseValue + IncreaseValuePerTier * ItemTier;
    }
}

[Serializable]
public class WeaponConfig
{
    public int BaseValue;
    public int IncreaseValuePerTier;

    public int DamageValue(int ItemLevel, ItemRarity ItemRarity)
    {
        if (ItemLevel < 0) ItemLevel = 0;
        float rarityMultiplier = 0;
        switch (ItemRarity)
        {
            case ItemRarity.Normal:
                rarityMultiplier = 1;
                break;
            case ItemRarity.Magic:
                rarityMultiplier = 1.5f;
                break;
            default:
                rarityMultiplier = 2;
                break;
        }
        return BaseValue + Mathf.RoundToInt(IncreaseValuePerTier * ItemLevel * rarityMultiplier); // 50 + 25 * 15 * 1
        // 50 + 25 * 15 * 1.5
    }
}

[Serializable]
public class ArmourConfig
{

}