using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3; //Per powerUp fisso modificare Max
    public int currentHealth; //Per consumabile modificare current

    [Header("Events")]
    [SerializeField] private UnityEvent _onPlayerDeath;

    private bool _isShielded = false;
    public int MaxHealth => maxHealth;

    public Action<int,int> _onHealthChange;

    private void Start()
    {
        SetHp(maxHealth);
    }
    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, maxHealth);
        if (hp != currentHealth)
        {
            currentHealth = hp;

            // _onHealthChange?.Invoke(_currentHealth, _maxHealth);
             _onHealthChange?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                _onPlayerDeath.Invoke();
            }
        }
    }
    public void RestoreFullHp() => SetHp(maxHealth);

    public void SetMaxHealth(int maxHealth)
    {
        //maxHealth = maxHealth;
        RestoreFullHp();
    }
    public void TakeDamage(float damage)
    {
        if (_isShielded) return;
        SetHp((int)(currentHealth - damage));
    }

    public void AddHp(int amount) => SetHp(currentHealth + amount);
    public void Heal(int amount) => AddHp(amount);

    public void AddMaxHits(int amount)
    {
        maxHealth += amount;
        _onHealthChange?.Invoke(currentHealth, maxHealth);
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
