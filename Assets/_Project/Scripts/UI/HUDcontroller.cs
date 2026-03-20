using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDcontroller : MonoBehaviour
{
    public static HUDcontroller Instance { get; private set; }

    [SerializeField] private GameObject _gameOverBanner;
    [SerializeField] private GameObject _hud;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _clickSound;

    [SerializeField] private AudioClip _gameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
       // if (AudioManager.Instance != null) AudioManager.Instance.PlayMusic(_backgroundMusic);
    }

    public void Restart()
    {
        _gameOverBanner.SetActive(false);
        SceneManager.LoadScene(2);
        _hud.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
    }

    public void StopAllSound()
    {
        AudioManager.Instance.StopAllAudioSource();
    }

    public void PlayGameOverMusic()
    {
        AudioManager.Instance.PlaySFX(_gameOver);
    }
}
