using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotShop : MonoBehaviour
{
    [SerializeField] private SO_PassivePowerUp _passiveItem;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _effectText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _buyButton;

    private void UIUpdate()
    {
        int level = PowerUpManager.Instance.GetLevel(_passiveItem);
        _levelText.SetText($"lvl : {level} / {_passiveItem.MaxLevel}");
        if( level >= _passiveItem.MaxLevel )
        {
            _buyButton.interactable = false;
            _costText.SetText($"MAX LEVEL");
        }
        else
        {
            int cost = _passiveItem.GetCost(level);
            _buyButton.interactable = CoinManager.Instance.GetCoins(_passiveItem.CoinType) > cost ? true : false;
            _costText.SetText(cost.ToString());
        }
    }

    private void OnBuyClick()
    {
        ShopManager.Instance.Buy(_passiveItem);
    }


}
