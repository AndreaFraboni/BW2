using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _femalePlayer;
    [SerializeField] private GameObject _malePlayer;

    private GameObject _currentPlayer;

    private void Start()
    {
        if(CharacterSelectionManager.Instance.SelectedType == CharacterSelectionManager.CharacterType.Female)
        {
            _currentPlayer = _femalePlayer;
            _femalePlayer.SetActive(true);
        }
        else
        {
            _currentPlayer = _malePlayer;
            _malePlayer.SetActive(true);
        }
        PlayerController playerController = _currentPlayer.GetComponent<PlayerController>();
        PlayerManager.Instance.SetPlayer(playerController);
    }
}
