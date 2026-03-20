using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericSingleton<PlayerManager>
{
    public PlayerController CurrentPlayer { get; private set; }
    public LifeController lifeControllerRef;

    public void SetPlayer(PlayerController player, LifeController curLifeController)
    {
        CurrentPlayer = player;
        lifeControllerRef = curLifeController;
    }
        
    public LifeController GetCurrentLifeController()
    {
        return lifeControllerRef;
    }
}

