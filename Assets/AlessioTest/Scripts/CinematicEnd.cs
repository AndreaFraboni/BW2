using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEnd : MonoBehaviour
{
    [SerializeField] private AnimatorLayerSwitcher _enemyAnimatorLayerSwitch;

    public Action OnActiveHUD;

    [SerializeField] private GameObject HUD;

    public void StartGameplay()
    {
        HUD.SetActive(true);
        TimeManager.Instance.SetGameStarted(true);

        PlayerManager.Instance.CurrentPlayer.enabled = true;
        PlayerManager.Instance.CurrentPlayer.AnimationParamHandler.AnimatorLayerSwitcher.SetGameplayMode();
        _enemyAnimatorLayerSwitch.SetGameplayMode();
       // PlayerManager.Instance.CurrentPlayer.transform.position = Vector3.zero;
        
    }
}
