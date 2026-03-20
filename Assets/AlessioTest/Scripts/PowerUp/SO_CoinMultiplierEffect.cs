using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/CoinMultiplierEffect")]
public class SO_CoinMultiplierEffect : SO_Effect
{
    [SerializeField] private int _multiplier = 2;

    public override void Apply(GameObject user) 
    { 
        CoinManager.Instance.SetMultiplier(_multiplier);
        
    }
}
