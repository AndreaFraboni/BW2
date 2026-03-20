using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericSingleton<PlayerManager>
{
    public PlayerController CurrentPlayer { get; private set; }
    public LifeController lifeControllerRef;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _inGameMusic;
    [SerializeField] private AudioClip _clickSound;

    private void Start()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayMusic(_inGameMusic);
    }

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

