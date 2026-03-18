using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public enum coinType { CYBERCOIN, NATURALCOIN, BLACKWHITECOIN }

    private int _coinValue = 1;
    public coinType _coin;

    public void Collect(PlayerController player)
    {
        player.AddScore(_coinValue, _coin);
        Destroy(gameObject);
    }
}
