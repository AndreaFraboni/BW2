using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private TextMeshProUGUI _currenLifeText;

    private void OnEnable()
    {
//        if (_lifeController == null) _lifeController = FindObjectOfType<LifeController>();
        if (_lifeController == null) _lifeController = PlayerManager.Instance.GetCurrentLifeController();

        //Debug.Log("mi attivo e mi registro per onLifechanged !!");
        _lifeController._onHealthChange += UpdateLifeText;

        UpdateLifeText(_lifeController.currentHealth, _lifeController.maxHealth); // forzo lettura e aggiornamento UI del numero di vite attuale
    }

    private void OnDisable()
    {
        _lifeController._onHealthChange -= UpdateLifeText;
    }

    private void UpdateLifeText(int lifeNum,int maxhealth)
    {
        //Debug.Log("SETTO NUMERO VITE !!");
        _currenLifeText.text = lifeNum.ToString();
    }

}

