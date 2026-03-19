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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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


}
