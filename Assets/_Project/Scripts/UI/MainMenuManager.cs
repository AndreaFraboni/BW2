using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _clickSound;


    private void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(_backgroundMusic);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
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
