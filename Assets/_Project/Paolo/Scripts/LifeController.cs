using System;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _life;
    [SerializeField] private int _maxLife = 3;
    [SerializeField] private AnimationParamHandler _animHandler;

    public Action<int> OnLifeChanged;
        
    public int CurrentLife => _life;

    private void Awake()
    {
        _life = _maxLife;

        if (_animHandler == null) _animHandler = GetComponent<AnimationParamHandler>();
    }

    public void TakeDamage()
    {
        _life--;

        if (_life <= 0)
        {
            // DEATH
           // _animHandler.Death();
        }

        OnLifeChanged?.Invoke(_life);
    }



}
