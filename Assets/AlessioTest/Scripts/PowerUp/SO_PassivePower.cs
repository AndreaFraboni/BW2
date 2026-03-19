using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/PassivePowerUp")]
public class SO_PassivePowerUp : SO_PowerUpItem
{
    [SerializeField] private SO_Effect[] _effectsPerLevel;
    [SerializeField] private int[] _costPerLevel;
    [SerializeField] private CoinType _coinType;

    public override bool IsConsumable => false;
    public CoinType CoinType => _coinType;
    public int MaxLevel => _effectsPerLevel.Length;

    public int GetCost(int currentLevel)
    {
        if (currentLevel < 0 || currentLevel >= _costPerLevel.Length) { return 0; }
        return _costPerLevel[currentLevel];
    }
    public void ApplyLevel(int level , GameObject user)
    {
        int index = level - 1;
        if (index < 0 || index >= _effectsPerLevel.Length) return;
        _effectsPerLevel[index].Apply(user);
    }
}

