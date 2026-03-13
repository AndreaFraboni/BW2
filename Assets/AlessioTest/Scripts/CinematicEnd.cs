using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEnd : MonoBehaviour
{
    [SerializeField] private PlayerControllerFix _playerControllerFix;
    [SerializeField] private AnimatorLayerSwitcher _playerAnimatorLayerSwitch;
    [SerializeField] private AnimatorLayerSwitcher _enemyAnimatorLayerSwitch;
    [SerializeField] private Transform _playerTransform;
    public void StartGameplay()
    {
        _playerControllerFix.enabled = true;
        _playerAnimatorLayerSwitch.SetGameplayMode();
        _enemyAnimatorLayerSwitch.SetGameplayMode();
        _playerTransform.position = Vector3.zero;
        
    }
}
