using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEnd : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AnimatorLayerSwitcher _playerAnimatorLayerSwitch;
    [SerializeField] private AnimatorLayerSwitcher _enemyAnimatorLayerSwitch;
    [SerializeField] private Transform _playerTransform;
    public void StartGameplay()
    {
        _playerController.enabled = true;
        _playerAnimatorLayerSwitch.SetGameplayMode();
        _enemyAnimatorLayerSwitch.SetGameplayMode();
        _playerTransform.position = Vector3.zero;
        
    }
}
