using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericSingleton<PlayerManager>
{
    public PlayerController CurrentPlayer { get; private set; }

    public void SetPlayer(PlayerController player)
    {
        CurrentPlayer = player;
    }
}

