using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    [SerializeField] private GameObject _mainMenuPanel; // Pannello principale con i 3 bottonizzzzzz
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _creditsButton;

    [Header("Credits UI")]
    //[SerializeField] private GameObject _creditsPanel; // Parte disattivato
    //[SerializeField] private Button _backButton;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _clickSound;

    private void Start()
    {
        _mainMenuPanel.SetActive(true);
        //_creditsPanel.SetActive(false);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(_backgroundMusic);
    }

    private void OnEnable()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
        _exitButton.onClick.AddListener(ExitGame);
        _creditsButton.onClick.AddListener(OpenCredits);
        //_backButton.onClick.AddListener(CloseCredits);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveListener(StartNewGame);
        _exitButton.onClick.RemoveListener(ExitGame);
        _creditsButton.onClick.RemoveListener(OpenCredits);
        //_backButton.onClick.RemoveListener(CloseCredits);
    }

    public void StartNewGame()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
        SceneManager.LoadScene(3);
    }

    public void OpenCredits()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
        //_mainMenuPanel.SetActive(false); // Nascondi menu
        //_creditsPanel.SetActive(true);  // Mostra crediti
    }

    public void CloseCredits()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
        //_creditsPanel.SetActive(false); // Nascondi crediti
        //_mainMenuPanel.SetActive(true);  // Torna al menu
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX(_clickSound);
        //Debug.Log("Simulazione di chiusura, manca la build!");
        //Application.Quit();
       #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
       #else
        Application.Quit();
       #endif   
    }
}
