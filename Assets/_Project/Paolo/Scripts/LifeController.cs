using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxHealth = 90;
    [SerializeField] private int _currentHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPlayerDeath;
    [SerializeField] private UnityEvent<int, int> _onHealthChange;

    public int MaxHealth => _maxHealth;

    private void Start()
    {
        SetHp(_maxHealth);
    }
    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHealth);
        if (hp != _currentHealth)
        {
            _currentHealth = hp;
            _onHealthChange?.Invoke(_currentHealth, _maxHealth);
            if (_currentHealth <= 0)
            {
                _onPlayerDeath.Invoke();
            }
        }
    }
    public void RestoreFullHp() => SetHp(_maxHealth);
    public void TakeDamage(float damage)
    {
        SetHp((int)(_currentHealth - damage));
    }

    public void AddHp(int amount)
    {
        SetHp(_currentHealth + amount);
    }
}
