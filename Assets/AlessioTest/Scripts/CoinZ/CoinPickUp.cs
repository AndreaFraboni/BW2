using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : PickUp
{
    [SerializeField] private int _value = 1;
    [SerializeField] private CoinType _coinType;

    protected override void OnPick(GameObject player)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance._pickUpCoin);

        base.OnPick(player);
        CoinManager.Instance.AddCoin(_value, _coinType);
    }
}
