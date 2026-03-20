using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ConsumablePowerUp")]
public class SO_ConsumablePowerUp : SO_PowerUpItem
{
    [SerializeField] private int _cost;
    [SerializeField] private CoinType _coinType;

    public bool IsItemPurchased { get; set; } = false; 
    public override bool IsConsumable => true;
    public int Cost => _cost;
    public CoinType CoinType => _coinType;

}
