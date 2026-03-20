using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMultiplierHandler : MonoBehaviour
{
    private int _multiplier = 1;
    public int Multiplier => _multiplier;

    public void SetMultiplier(int multiplier)
    {
        _multiplier = multiplier;
    }

    public void ResetMultiplier()
    {
        _multiplier = 1;
    }
}
