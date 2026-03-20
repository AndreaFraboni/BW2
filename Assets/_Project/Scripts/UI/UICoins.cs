using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UICoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;

    private void CoinUpdate(int coin , CoinType coinType)
    {
        _coinText.SetText(coin.ToString());
    }

    private void OnEnable()
    {
        CoinManager.Instance.CoinChanged += CoinUpdate;
    }

    private void OnDisable()
    {
        if (CoinManager.Instance != null)
        CoinManager.Instance.CoinChanged -= CoinUpdate;
    }
}
