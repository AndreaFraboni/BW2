using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    [Header("Cinematic Cameras (One Shot)")]
    [SerializeField] private CinemachineVirtualCamera[] _cinematicCameras;
    [SerializeField] private float[] _cinematicDurations;
    [SerializeField] private bool _isCamPerspective = true;

    [Header("DisactivedObjectDuringCinematic")]
    [SerializeField] private GameObject _UI;

    [SerializeField] private CinemachineVirtualCamera _gameplayCamera;

    private int _gameplayPriority = 10;

    private int _cinematicPriority = 20;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Start()
    {
        SetUpCams();
    }

    private void SetUpCams()
    {
        foreach (var cam in _cinematicCameras) cam.Priority = 0;

        _mainCamera.orthographic = true;
    }

    public void ReturnToGameplayCamera()
    {
        foreach (var cam in _cinematicCameras) cam.Priority = 0;

        _gameplayCamera.Priority = _gameplayPriority;

        SetActiveObjectsDuringCinematic(true);

        _mainCamera.orthographic = true;
    }

    private void SetActiveObjectsDuringCinematic(bool isActive)
    {
        _UI.SetActive(isActive);
       
    }

    public void PlayCinematic(int cinematicIndex)
    {
        StartCoroutine(CinematicRoutine(cinematicIndex));
    }

    private IEnumerator CinematicRoutine(int cinematicIndex)
    {
        CinemachineVirtualCamera cinematicCam = _cinematicCameras[cinematicIndex];

        SetActiveObjectsDuringCinematic(false);

        if (_isCamPerspective) _mainCamera.orthographic = false;

        cinematicCam.Priority = _cinematicPriority;

        _gameplayCamera.Priority = 0;

        yield return new WaitForSeconds(_cinematicDurations[cinematicIndex]);

        SetActiveObjectsDuringCinematic(true);

        if (_isCamPerspective) _mainCamera.orthographic = true;

        cinematicCam.Priority = 0;
        _gameplayCamera.Priority = _gameplayPriority;

    }
}
