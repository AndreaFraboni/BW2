using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ShieldEffect")]
public class SO_ShieldEffect : SO_Effect
{
    [SerializeField] private float _duration = 5f;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent(out LifeController lifeController))
            lifeController.ActivateShield(_duration);
    }
}
