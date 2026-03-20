using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotShopPassive : UI_SlotShop
{
    [SerializeField] private SO_PassivePowerUp _passivePowerUp;
    protected override void OnBuyClick()
    {
        ShopManager.Instance.Buy(_passivePowerUp);
    }

    protected override void SetUp()
    {
        base.SetUp();
        _nameText.SetText(_passivePowerUp.Name);
    }
    protected override void UIUpdate()
    {
        int level = PowerUpManager.Instance.GetLevel(_passivePowerUp);
        _levelText.SetText($"lvl : {level} / {_passivePowerUp.MaxLevel}");
        if (level >= _passivePowerUp.MaxLevel)
        {
            _buyButton.interactable = false;
            _costText.SetText($"MAX");
        }
        else
        {
            int cost = _passivePowerUp.GetCost(level);
            _buyButton.interactable = CoinManager.Instance.CanAfford(cost, _passivePowerUp.CoinType);
            _costText.SetText(cost.ToString());
        }
    }

    
}
