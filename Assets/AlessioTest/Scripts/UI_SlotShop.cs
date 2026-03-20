using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_SlotShop : MonoBehaviour
{
   
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _levelText;
    [SerializeField] protected TextMeshProUGUI _costText;
    [SerializeField] protected TextMeshProUGUI _effectText;
    [SerializeField] protected TextMeshProUGUI _descriptionText;
    [SerializeField] protected Button _buyButton;

    private void Start()
    {
        SetUp();

    }

    private void OnDestroy()
    {
        if (PowerUpManager.Instance != null) 
        {
            PowerUpManager.Instance.OnPowerUp -= UIUpdate;
        }
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.OnItemPurchased -= UIUpdate;
        }
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.CoinChanged -= OnCoinChange;
        }
        
    }
    protected abstract void UIUpdate();
    
    private void OnCoinChange(int coin , CoinType coinType)
    {
        UIUpdate();
    }
    protected abstract void OnBuyClick();
    
       
    

    protected virtual void SetUp()
    {
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(OnBuyClick);
        UIUpdate();
        PowerUpManager.Instance.OnPowerUp += UIUpdate;
        ShopManager.Instance.OnItemPurchased += UIUpdate;
        CoinManager.Instance.CoinChanged += OnCoinChange;
       
    }


}
