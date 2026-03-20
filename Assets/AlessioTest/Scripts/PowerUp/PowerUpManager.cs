using System;
using UnityEngine;

public class PowerUpManager : GenericSingleton<PowerUpManager>
{
    [SerializeField] private SO_PassivePowerUp[] _passivePowerUps;

    public event Action OnPowerUp;

    private int[] _levels;
    private bool[] _owned;

    protected override void Awake()
    {
        base.Awake();
        int length = _passivePowerUps.Length;

        _levels = new int[length];
        _owned = new bool[length];

        for (int i = 0; i < length; i++)
        {
            _levels[i] = 0;
            _owned[i] = false;
        }
    }
    protected override bool ShouldBeDestroyOnLoad()
    {
        return false;
        
    }
    public bool Upgrade(SO_PassivePowerUp powerUp)
    {
        int index = FindIndex(powerUp);
        if (index == -1) return false;

        int currentLevel = _levels[index];

        if (currentLevel >= powerUp.MaxLevel)
            return false;

        int cost = powerUp.GetCost(currentLevel);

       
        if (!CoinManager.Instance.CanAfford(cost, powerUp.CoinType))
            return false;

        _levels[index]++;
        _owned[index] = true;

        OnPowerUp?.Invoke();

        return true;
    }

    public void ApplyAllOwned()
    {
        for (int i = 0; i < _passivePowerUps.Length; i++)
        {
            if (_owned[i] && _levels[i] > 0)
            {
                _passivePowerUps[i].ApplyLevel(
                    _levels[i],
                    PlayerManager.Instance.CurrentPlayer.gameObject
                );
            }
        }
    }

    private int FindIndex(SO_PassivePowerUp powerUp)
    {
        for (int i = 0; i < _passivePowerUps.Length; i++)
        {
            if (_passivePowerUps[i] == powerUp)
                return i;
        }

        return -1;
    }

    public int GetLevel(SO_PassivePowerUp powerUp)
    {
        int index = FindIndex(powerUp);
        return index == -1 ? 0 : _levels[index];
    }

    public bool IsOwned(SO_PassivePowerUp powerUp)
    {
        int index = FindIndex(powerUp);
        return index != -1 && _owned[index];
    }


    public void Save()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            PlayerPrefs.SetInt("PowerUp_Level_" + i, _levels[i]);
        }

        PlayerPrefs.Save();
    }

    public void Load()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i] = PlayerPrefs.GetInt("PowerUp_Level" + i, 0);
            _owned[i] = _levels[i] > 0;
        }
    }
}
