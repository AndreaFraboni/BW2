using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType { CyberCoin, NaturalCoin, BlackWhiteCoin }

public class CoinManager : GenericSingleton<CoinManager>
{

    [SerializeField] private int _cyberCoins;
    [SerializeField] private int _naturalCoins;
    [SerializeField] private int _blackWhiteCoins;

    private int _multiplier = 1;
    public int Multiplier => _multiplier;

    public void SetMultiplier(int multiplier) => _multiplier = multiplier;
    public void ResetMultiplier() => _multiplier = 1;

    public void AddCoin(int value, CoinType type)
    {
        int finalValue = value * _multiplier;

        switch (type)
        {
            case CoinType.CyberCoin: _cyberCoins += value; break;
            case CoinType.NaturalCoin: _naturalCoins += value; break;
            case CoinType.BlackWhiteCoin: _blackWhiteCoins += value; break;
        }
    }

    public int GetCoins(CoinType type) => type switch
    {
        CoinType.CyberCoin => _cyberCoins,
        CoinType.NaturalCoin => _naturalCoins,
        CoinType.BlackWhiteCoin => _blackWhiteCoins,
        _ => 0
    };

    public bool Spend(int amount, CoinType type)
    {
        if (GetCoins(type) < amount) return false;
        switch (type)
        {
            case CoinType.CyberCoin: _cyberCoins -= amount; break;
            case CoinType.NaturalCoin: _naturalCoins -= amount; break;
            case CoinType.BlackWhiteCoin: _blackWhiteCoins -= amount; break;
        }
        return true;
    }
}
