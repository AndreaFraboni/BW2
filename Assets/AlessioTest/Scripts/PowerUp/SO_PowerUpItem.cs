using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SO_PowerUpItem : SO_GenericItem
{
    [SerializeField] private SO_Effect _effect;

    public abstract bool IsConsumable { get; }

    public override void Use(GameObject user)
    {
        _effect.Apply(user);
        if (IsConsumable)
            InventoryManager.Instance.RemoveItem(this);
    }
}
