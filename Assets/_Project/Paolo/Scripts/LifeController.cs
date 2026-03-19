using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxHealth = 3; //Per powerUp fisso modificare Max
    [SerializeField] private int _currentHealth; //Per consumabile modificare current

    [Header("Events")]
    [SerializeField] private UnityEvent _onPlayerDeath;
    [SerializeField] private UnityEvent<int, int> _onHealthChange;

    private bool _isShielded = false;
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

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        RestoreFullHp();
    }
    public void TakeDamage(float damage)
    {
        if (_isShielded) return;
        SetHp((int)(_currentHealth - damage));
    }

    public void AddHp(int amount) => SetHp(_currentHealth + amount);
    public void Heal(int amount) => AddHp(amount);

    public void AddMaxHits(int amount)
    {
        _maxHealth += amount;
        _onHealthChange?.Invoke(_currentHealth, _maxHealth);
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldCoroutine(duration));
    }

    private IEnumerator ShieldCoroutine(float duration)
    {
        _isShielded = true;
        yield return new WaitForSeconds(duration);
        _isShielded = false;
    }
}
