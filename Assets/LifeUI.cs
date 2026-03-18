using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private TextMeshProUGUI _currenLifeText;

    private void OnEnable()
    {
        if (_lifeController == null) _lifeController = FindObjectOfType<LifeController>();

        Debug.Log("mi attivo e mi registro per onLifechanged !!");
        _lifeController.OnLifeChanged += UpdateLifeText;

        UpdateLifeText(_lifeController.CurrentLife); // forzo lettura e aggiornamento UI del numero di vite attuale
    }

    private void OnDisable()
    {
        _lifeController.OnLifeChanged -= UpdateLifeText;
    }

    private void UpdateLifeText(int lifeNum)
    {
        Debug.Log("SETTO NUMERO VITE !!");
        _currenLifeText.text = lifeNum.ToString();
    }

}
