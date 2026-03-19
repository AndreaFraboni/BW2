using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : GenericSingleton<PowerUpManager>
{
    [SerializeField] private SO_PassivePowerUp[] _passivePowerUps;
    public event Action OnPowerUp;
    
    private int[] _levels;

    private void Awake()
    {
        _levels = new int[_passivePowerUps.Length];
    }

    public bool Upgrade (SO_PassivePowerUp powerUp)
    {
        int index = FindIndex(powerUp);
        if (index == -1) return false;

        int currentLevel = _levels[index];
        if (currentLevel >= powerUp.MaxLevel) return false;

        int cost = powerUp.GetCost(currentLevel);

        _levels[index]++;
        powerUp.ApplyLevel(_levels[index], PlayerManager.Instance.CurrentPlayer.gameObject);
        return true;
    }

    private int FindIndex (SO_PassivePowerUp powerUp)
    {
        for (int i = 0; i < _passivePowerUps.Length; i++)
        {
            if (_passivePowerUps[i] == powerUp) return i;
        }
        return -1;
    }

    public int GetLevel(SO_PassivePowerUp powerUp)
    {
        int index = FindIndex(powerUp);
        return index == -1 ? 0 : _levels[index];
    }
}
