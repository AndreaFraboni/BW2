using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotShopConsumable : UI_SlotShop
{
    [SerializeField] private SO_ConsumablePowerUp _consumablePowerUp;
    protected override void OnBuyClick()
    {
        ShopManager.Instance.Buy(_consumablePowerUp);
    }

    protected override void SetUp()
    {
        base.SetUp();
        _nameText.SetText(_consumablePowerUp.Name);
    }

    protected override void UIUpdate()
    {
        int cost = _consumablePowerUp.Cost;
        _buyButton.interactable = CoinManager.Instance.CanAfford(cost , _consumablePowerUp.CoinType);
        _buyButton.interactable = !_consumablePowerUp.IsItemPurchased;

        _costText.SetText(cost.ToString());
    }
}
