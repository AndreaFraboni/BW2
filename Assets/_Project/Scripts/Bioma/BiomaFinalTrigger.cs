using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomaFinalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TileSpawner.Instance.ChangeBioma();
        }
    }


}
