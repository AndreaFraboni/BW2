using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameplayCam;

    private void Start()
    {
        _gameplayCam.Follow = PlayerManager.Instance.CurrentPlayer.transform;
    }
}
