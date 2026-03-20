using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ExtraHitEffect")]
public class SO_ExtraHitEffect : SO_Effect
{
    [SerializeField] private int _extraHits = 1;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent(out LifeController lifeController))
            lifeController.AddMaxHits(_extraHits);
    }
}
