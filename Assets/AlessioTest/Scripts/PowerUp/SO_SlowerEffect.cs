using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/SlowEffect")]
public class SO_SlowEffect : SO_Effect
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _slowMultiplier = 0.5f;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent(out PlayerController playerController))
            playerController.ActivateSlow(_duration, _slowMultiplier);
    }
}
