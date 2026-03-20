using System;
using UnityEngine;

public enum CoinType { CyberCoin, NaturalCoin, BlackWhiteCoin }

public class CoinManager : GenericSingleton<CoinManager>
{
    [SerializeField] private int _cyberCoins;
    [SerializeField] private int _naturalCoins;
    [SerializeField] private int _blackWhiteCoins;

    public event Action<int, CoinType> CoinChanged;

    private int _multiplier = 1;
    public int Multiplier => _multiplier;

    public void SetMultiplier(int multiplier) => _multiplier = multiplier;
    public void ResetMultiplier() => _multiplier = 1;

    public void AddCoin(int value, CoinType type)
    {
        int finalValue = value * _multiplier;
        Debug.Log(_multiplier);

        switch (type)
        {
            case CoinType.CyberCoin:
                _cyberCoins += finalValue;
                CoinChanged?.Invoke(_cyberCoins, type);
                break;

            case CoinType.NaturalCoin:
                _naturalCoins += finalValue;
                CoinChanged?.Invoke(_naturalCoins, type);
                break;

            case CoinType.BlackWhiteCoin:
                _blackWhiteCoins += finalValue;
                CoinChanged?.Invoke(_blackWhiteCoins, type);
                break;
        }
    }

    public int GetCoins(CoinType type) => type switch
    {
        CoinType.CyberCoin => _cyberCoins,
        CoinType.NaturalCoin => _naturalCoins,
        CoinType.BlackWhiteCoin => _blackWhiteCoins,
        _ => 0
    };

    public bool CanAfford(int amount, CoinType type)
    {
        return GetCoins(type) >= amount;
    }

    public bool Spend(int amount, CoinType type)
    {
        if (!CanAfford(amount, type)) return false;

        switch (type)
        {
            case CoinType.CyberCoin:
                _cyberCoins -= amount;
                CoinChanged?.Invoke(_cyberCoins, type);
                break;

            case CoinType.NaturalCoin:
                _naturalCoins -= amount;
                CoinChanged?.Invoke(_naturalCoins, type);
                break;

            case CoinType.BlackWhiteCoin:
                _blackWhiteCoins -= amount;
                CoinChanged?.Invoke(_blackWhiteCoins, type);
                break;
        }

        return true;
    }
}