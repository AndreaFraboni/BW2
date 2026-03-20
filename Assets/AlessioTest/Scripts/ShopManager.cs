using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : GenericSingleton<ShopManager>
{
    public event Action OnItemPurchased;
    public bool CanBuy(SO_PowerUpItem item)
    {
        if (item.IsConsumable)
        {
            var consumable = (SO_ConsumablePowerUp)item;
            return CoinManager.Instance.GetCoins(consumable.CoinType) >= consumable.Cost;
        }
        else
        {
            var passive = (SO_PassivePowerUp)item;
            int currentLevel = PowerUpManager.Instance.GetLevel(passive);
            if (currentLevel >= passive.MaxLevel) return false;
            return CoinManager.Instance.GetCoins(passive.CoinType) >= passive.GetCost(currentLevel);
        }
    }

    public void Buy(SO_PowerUpItem item)
    {
        if (!CanBuy(item)) return;

        if (item.IsConsumable)
            BuyConsumable((SO_ConsumablePowerUp)item);
        else
            BuyPassive((SO_PassivePowerUp)item);

        OnItemPurchased?.Invoke();
    }

    private void BuyPassive(SO_PassivePowerUp powerUp)
    {
        int currentLevel = PowerUpManager.Instance.GetLevel(powerUp);
        CoinManager.Instance.Spend(powerUp.GetCost(currentLevel), powerUp.CoinType);
        PowerUpManager.Instance.Upgrade(powerUp);
    }

    private void BuyConsumable(SO_ConsumablePowerUp consumable)
    {
        CoinManager.Instance.Spend(consumable.Cost, consumable.CoinType);
        InventoryManager.Instance.AddItem(consumable);
    }
}
