using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOver;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _gameBackgroundMusic;
    [SerializeField] private AudioClip _clickSound;

    //private PlayerController _pc;

    private void Start()
    {
       // if (AudioManager.Instance != null) AudioManager.Instance.PlayMusic(_gameBackgroundMusic);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (_pc.isPaused)
            //{
            //    Resume();
            //}
            //else
            //{
            //    Pause();
            //}
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0.0f;

        //_pc.isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

        //_pc.isPaused = false;
        

    }

    public void Restart()
    {
        AudioManager.Instance.StopAllAudioSource();
        gameOver.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        AudioManager.Instance.StopAllAudioSource();

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

}
