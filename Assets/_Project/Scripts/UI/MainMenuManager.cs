using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _clickSound;
    private void Start()
    {
        LoadAudioSettings();
        if (AudioManager.Instance != null) AudioManager.Instance.PlayMusic(_backgroundMusic);
    }

    private void LoadAudioSettings()
    {
        float masterVolume = 1f;
        float musicVolume = 1f;
        float sfxVolume = 1f;

        //Debug.Log("MAIN MENU Call LOADER AUDIO SETTINGS !!!");
        bool result = IOManager.Instance.LoadAudioSettings(ref masterVolume, ref musicVolume, ref sfxVolume);

        if (result)
        {
            AudioManager.Instance.SetMasterVolume(masterVolume);
            AudioManager.Instance.SetMusicVolume(musicVolume);
            AudioManager.Instance.SetSFXVolume(sfxVolume);
        }
        else
        {
            Debug.Log("ERROR AUDIO SETTINGS NOT LOADED !!!");
        }
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
    }

    public void CharacterSelection()
    {
        AudioManager.Instance.StopAllAudioSource();
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {
        AudioManager.Instance.StopAllAudioSource();
        SceneManager.LoadScene(3);
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
