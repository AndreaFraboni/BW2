using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/MagnetEffect")]
public class SO_MagnetEffect : SO_Effect
{
    [SerializeField] private float _duration = 5f;

    public override void Apply(GameObject user)
    {
        PickUpDetector detector = user.GetComponentInChildren<PickUpDetector>();
        if (detector != null) 
            detector.ActivateMagnet(_duration);
    }
}