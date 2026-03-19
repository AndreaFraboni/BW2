using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/MagnetEffect")]
public class SO_MagnetEffect : SO_Effect
{
    [SerializeField] private float _duration = 5f;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent(out PickUpDetector detector))
            detector.ActivateMagnet(_duration);
    }
}