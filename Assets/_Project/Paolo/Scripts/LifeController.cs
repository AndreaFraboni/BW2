using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _life;
    [SerializeField] private int _maxLife = 3;

    private void Awake()
    {
        _life = _maxLife;
    }

    public void SetLife(int life)
    {
        _life = Mathf.Clamp(life, 0, _maxLife);

        if (_life == 0)
        {
            //aggiungere funzione per la sconfitta
        }
    }

    public void TakeDamage(int damage) => SetLife(_life - damage);
}
