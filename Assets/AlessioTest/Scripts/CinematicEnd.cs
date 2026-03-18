using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEnd : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AnimatorLayerSwitcher _playerAnimatorLayerSwitch;
    [SerializeField] private AnimatorLayerSwitcher _enemyAnimatorLayerSwitch;
    [SerializeField] private Transform _playerTransform;

    public Action OnActiveHUD;

    [SerializeField] private GameObject HUD;

    public void StartGameplay()
    {
        HUD.SetActive(true);
        TimeManager.Instance.SetGameStarted(true);

        _playerController.enabled = true;
        _playerAnimatorLayerSwitch.SetGameplayMode();
        _enemyAnimatorLayerSwitch.SetGameplayMode();
        _playerTransform.position = Vector3.zero;
        
    }
}
