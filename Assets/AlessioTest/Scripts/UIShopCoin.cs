using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShopCoin : MonoBehaviour
{

    [SerializeField] private TMP_Text _cyberText;
    [SerializeField] private TMP_Text _natureText;
    [SerializeField] private TMP_Text _blackWhiteText;

    private void OnEnable()
    {
        CoinManager.Instance.CoinChanged += UpdateDisplay;
        UpdateAll();
    }

    private void OnDisable()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.CoinChanged -= UpdateDisplay;
    }

    private void UpdateAll()
    {
        _cyberText.text = CoinManager.Instance.GetCoins(CoinType.CyberCoin).ToString();
        _natureText.text = CoinManager.Instance.GetCoins(CoinType.NaturalCoin).ToString();
        _blackWhiteText.text = CoinManager.Instance.GetCoins(CoinType.BlackWhiteCoin).ToString();
    }

    private void UpdateDisplay(int amount, CoinType type)
    {
        switch (type)
        {
            case CoinType.CyberCoin: _cyberText.text = amount.ToString(); break;
            case CoinType.NaturalCoin: _natureText.text = amount.ToString(); break;
            case CoinType.BlackWhiteCoin: _blackWhiteText.text = amount.ToString(); break;
        }
    }
}
