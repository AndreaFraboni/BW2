using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/HealEffect")]
public class SO_HealEffect : SO_Effect
{
    [SerializeField] private int _healAmount = 1;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent(out LifeController lifeController))
            lifeController.Heal(_healAmount);
    }
}