using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _femalePlayer;
    [SerializeField] private GameObject _malePlayer;
    [SerializeField] private GameObject _femaleEnemy;
    [SerializeField] private GameObject _maleEnemy;

    private GameObject _currentPlayer;
    private LifeController _lf;

    private void Awake()
    {
        if(CharacterSelectionManager.Instance.SelectedType == CharacterSelectionManager.CharacterType.Female)
        {
            _currentPlayer = _femalePlayer;
            _femalePlayer.SetActive(true);
            _maleEnemy.SetActive(true);
        }
        else
        {
            _currentPlayer = _malePlayer;
            _malePlayer.SetActive(true);
            _femaleEnemy.SetActive(true);
        }
        PlayerController playerController = _currentPlayer.GetComponent<PlayerController>();
        LifeController _lf = _currentPlayer.GetComponent<LifeController>();
        PlayerManager.Instance.SetPlayer(playerController,_lf);
    }
}
